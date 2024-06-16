using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UserSystemFramework.Scripts.System.Config;
using UserSystemFramework.Scripts.System.Controllers.Attributes;
using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;
using UserSystemFramework.Scripts.System.Utilities;

namespace UserSystemFramework.Scripts.System.Services.Classes
{
    public class ServerRequestSenderService : Service
    {
        public override ServicePriority Priority => ServicePriority.High;
        public ServerConfiguration ServerConfig => ConfigService.ServerConfig;
        public static bool CanConnect = false;
        private IRequest _sendRequest;

        public override void Init()
        {
            base.Init();
            OnSceneReady += Ready;
        }
        public override void OnDisable()
        {
            base.OnDisable();
            OnSceneReady -= Ready;
        }
        private void Ready()
        {
            InitializeServerConnection();
        }

        private async void InitializeServerConnection()
        {
            CanConnect = false;
            _sendRequest = ServerRequestGetterService.Get(RequestType.InitialRequest, false, true);
            _sendRequest = await SendRequest(_sendRequest, InitializeServerConnectionCallback);
        }

        private void InitializeServerConnectionCallback(IRequest request)
        {
            bool isSuccess = request.ResultType == RequestResultType.Success;
            switch (isSuccess)
            {
                case true:
                    DebugService.LogImportant("Database connection initialized successfully", DebuggingLevel.VeryImportantOnly);
                    CanConnect = true;
                    EventPublisher.TriggerServerInitialized();
                    break;
                default:
                    DebugService.LogError("Database connection initialization failed", DebuggingLevel.ErrorsOnly);
                    break;
            }
            CanConnect = isSuccess;
        }
        public async Task<IRequest> SendRequest(IRequest request, Action<IRequest> callback = null, int callbackDelay = 0, bool repeatIfFailed = true, [CallerMemberName] string callerName = "")
        {
            request.ConnectionStartTime = DateTime.Now;
            Dictionary<string, string> paramDict = new Dictionary<string, string>();
            foreach (KeyValuePair<string,string> requestConnectionParameter in request.ConnectionParameters)
            {
                try
                {
                    paramDict.Add(requestConnectionParameter.Key, requestConnectionParameter.Value.ToDecrypted().ToEncrypted());
                }
                catch (Exception)
                {
                    paramDict.Add(requestConnectionParameter.Key, requestConnectionParameter.Value.ToEncrypted());
                }
            }
            if (!request.ConnectionParameters.ContainsKey("token"))
            {
                paramDict.Add("token", request.Token.ToEncrypted());
            }

            if (LocalAccountController.CurrentLocalUser != null && !String.IsNullOrEmpty(LocalAccountController.CurrentLocalUser.UserToken))
            {
                paramDict["token"] = LocalAccountController.CurrentLocalUser.UserToken;
            }
            request.ConnectionParameters = paramDict;
            UnityWebRequest webRequest = UnityWebRequest.Post(ServerConfig.serverUrl + request.ConnectionEndpoint, request.ConnectionParameters);
            DebugService.Log($"Sending Request with details:{request.DebugRequest()}", DebuggingLevel.Everything);
            await webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                if(repeatIfFailed)
                {
                    DebugService.LogError("Request threw an error. Requesting again.", DebuggingLevel.ErrorsOnly);
                    return await SendRequest(request, callback, callbackDelay, false, callerName);
                }
                else
                {
                    DebugService.LogError("Request threw an error. Requesting again is disabled.", DebuggingLevel.ErrorsOnly);
                    request.ConnectionFinishTime = DateTime.Now;
                    request.ConnectionResultText = webRequest.error;
                    DebugService.LogError($"Request:{request.DebugRequest()}", DebuggingLevel.ErrorsOnly);
                    return request;
                }
            }
            if (String.IsNullOrEmpty(webRequest.error))
            {
                request.ConnectionFinishTime = DateTime.Now;
                request.ConnectionResultText = webRequest.downloadHandler.text.ToDecrypted();
                Dictionary<string, string> headerDict = new Dictionary<string, string>();
                foreach (KeyValuePair<string,string> requestConnectionHeader in webRequest.GetResponseHeaders())
                {
                    headerDict.Add(requestConnectionHeader.Key, requestConnectionHeader.Value.ToDecrypted());
                }
                request.ConnectionResponseHeaders = headerDict;
                DebugService.Log("Request completed successfully", DebuggingLevel.AllDebug);
                DebugService.Log($"Request details:{request.DebugRequest()}", DebuggingLevel.Everything);
                if (callback != null && ServiceBootstrap.IsGameRunning)
                {
                    await Task.Delay(callbackDelay);
                    if (ServiceBootstrap.IsGameRunning)
                    {
                        if (callerName == callback.Method.Name)
                        {
                            DebugService.LogWarning($"Calling a recurring method with name {callerName}", DebuggingLevel.Everything);
                        }
                        RequestCallbackMethodAttribute callbackAttribute =
                            (RequestCallbackMethodAttribute)callback.Method
                                .GetCustomAttribute(typeof(RequestCallbackMethodAttribute));
                        if(callbackAttribute != null)
                        {
                            callbackAttribute.CallbackDebug(request);
                        }
                        callback.Invoke(request);
                    }
                }
            }
            else
            {
                request.ConnectionFinishTime = DateTime.Now;
                request.ConnectionResultText = webRequest.error;
                request.ConnectionResponseHeaders = webRequest.GetResponseHeaders();
                DebugService.Log($"Request encrypted return:{webRequest.downloadHandler.text}", DebuggingLevel.Everything);
                DebugService.LogError("Request threw an error. Please handle this.", DebuggingLevel.ErrorsOnly);
                DebugService.LogError($"Request:{request.DebugRequest()}", DebuggingLevel.ErrorsOnly);
            }
            webRequest.Dispose();
            return request;
        }
    }
}
