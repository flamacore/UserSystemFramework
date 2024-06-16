using System;
using System.Collections.Generic;
using UnityEngine;
using UserSystemFramework.Scripts.System.Config;
using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Services.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;
using UserSystemFramework.Scripts.System.Utilities;

namespace UserSystemFramework.Scripts.System.Structure.Classes
{
    public enum RequestResultType
    {
        Success,
        Neutral,
        Fail,
        Undefined
    }
    public struct Request : IRequest
    {
        public bool IsSet { get; set; }
        public string Token
        {
            get
            {
                if (ConnectionResponseHeaders != null && ConnectionResponseHeaders.ContainsKey("userToken"))
                {
                    return ConnectionResponseHeaders["userToken"];
                }
                else if (ConnectionParameters != null && ConnectionParameters.ContainsKey("userToken"))
                {
                    return ConnectionParameters["userToken"];
                }
                else
                {
                    return "";
                }
            }
        }
        public DateTime ConnectionStartTime { get; set; }
        public DateTime ConnectionFinishTime { get; set; }
        public string ConnectionEndpoint { get; set; }

        public float ConnectionTotalTime => (float)ConnectionFinishTime.Subtract(ConnectionStartTime).Milliseconds;

        public Dictionary<string, string> ConnectionParameters { get; set; }
        public ServerConfiguration Configuration { get; set; }
        public string ConnectionResultText { get; set; }
        public Dictionary<string, string> ConnectionResponseHeaders { get; set; }
        public RequestType RequestEnum { get; set; }

        public RequestResultType ResultType
        {
            get
            {
                if (!String.IsNullOrEmpty(ConnectionResultText))
                {
                    RequestResultType resultType;
                    if (ConnectionResultText.ContainsErrorCode()) resultType = RequestResultType.Fail;
                    else if (ConnectionResultText.ContainsNeutralCode()) resultType = RequestResultType.Neutral;
                    else if (ConnectionResultText.ContainsSuccessCode()) resultType = RequestResultType.Success;
                    else resultType = RequestResultType.Undefined;
                    return resultType;
                }
                else if (ConnectionResponseHeaders != null && ConnectionResponseHeaders.ContainsKey("RequestResult"))
                {
                    if(!String.IsNullOrEmpty(ConnectionResponseHeaders["RequestResult"]))
                    {
                        RequestResultType resultType;
                        if (ConnectionResponseHeaders["RequestResult"].ToDecrypted().ContainsErrorCode()) resultType = RequestResultType.Fail;
                        else if (ConnectionResponseHeaders["RequestResult"].ToDecrypted().ContainsNeutralCode()) resultType = RequestResultType.Neutral;
                        else if (ConnectionResponseHeaders["RequestResult"].ToDecrypted().ContainsSuccessCode()) resultType = RequestResultType.Success;
                        else resultType = RequestResultType.Undefined;
                        return resultType;
                    }
                    else
                    {
                        return RequestResultType.Undefined;
                    }
                }
                else
                {
                    return RequestResultType.Undefined;
                }
            }
        }

        public string DebugRequest()
        {
            string connParams = "";
            foreach (KeyValuePair<string, string> parameter in ConnectionParameters)
            {
                connParams += $"Parameter({parameter.Key}): {parameter.Value.ToDecrypted()}, \n";
            }
            string headers = "";
            foreach (KeyValuePair<string, string> parameter in ConnectionResponseHeaders)
            {
                headers += $"Header({parameter.Key}): {parameter.Value.ToDecrypted()}, \n";
            }
            return $"Request Token:{Token} ConnectionStart Time:{ConnectionStartTime:hh:mm:ss.fff tt} \n" +
                   $"Connection Finish Time:{ConnectionFinishTime:hh:mm:ss.fff tt} \n" +
                   (ConnectionTotalTime > 0 ? $"Connection Total Time:{ConnectionTotalTime} \n" : "") +
                   $"Connection Endpoint:{ConnectionEndpoint} \n" +
                   $"Connection Result Text:{ConnectionResultText} \n" +
                   $"Parameters: {connParams} \n" +
                   $"Headers: {headers} \n";
        }

        public Request(string endPoint, ServerConfiguration config, Dictionary<string, string> parameters, RequestType requestEnum) : this()
        {
            ConnectionEndpoint = endPoint;
            ConnectionStartTime = default;
            ConnectionFinishTime = default;
            ConnectionResultText = null;
            Configuration = config;
            ConnectionParameters = parameters;
            RequestEnum = requestEnum;
            ConnectionResponseHeaders = new Dictionary<string, string>();
            ConnectionResultText = "";
            ConnectionParameters.Add("dbEnv", Configuration.databaseEnvironment.ToString());
            ConnectionParameters.Add("dbName", Configuration.databaseName);
            ConnectionParameters.Add("dbVer", Configuration.databaseVersion);
            DebugService.Log($"New request generated for {endPoint} {DebugRequest()}", DebuggingLevel.Everything);
        }

        public void CopyTokenFromRequest(IRequest requestToCopy)
        {
            ConnectionResponseHeaders = requestToCopy.ConnectionResponseHeaders;
            if (ConnectionParameters != null)
            {
                if (!ConnectionParameters.ContainsKey("token"))
                {
                    ConnectionParameters.Add("token", ConnectionResponseHeaders["userToken"]);
                }
                else
                {
                    ConnectionParameters["token"] = ConnectionResponseHeaders["userToken"];
                }
            }
        }
        public void GetTokenFromLocalUser()
        {
            if (ConnectionParameters != null)
            {
                if (!ConnectionParameters.ContainsKey("token"))
                {
                    ConnectionParameters.Add("token", LocalAccountController.CurrentLocalUser.UserToken);
                }
                else
                {
                    ConnectionParameters["token"] = LocalAccountController.CurrentLocalUser.UserToken;
                }
            }
        }
        
        public void SetToken(string token)
        {
            if (ConnectionParameters != null)
            {
                if (!ConnectionParameters.ContainsKey("token"))
                {
                    ConnectionParameters.Add("token", token);
                }
                else
                {
                    ConnectionParameters["token"] = token;
                }
            }
        }
    }
}