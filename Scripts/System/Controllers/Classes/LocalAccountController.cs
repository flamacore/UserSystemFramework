using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UserSystemFramework.Scripts.API.Interfaces;
using UserSystemFramework.Scripts.API.ObjectUpdaters;
using UserSystemFramework.Scripts.System.Controllers.Attributes;
using UserSystemFramework.Scripts.System.Controllers.Interfaces;
using UserSystemFramework.Scripts.System.Data.Classes;
using UserSystemFramework.Scripts.System.Data.Enums;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Services.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;
using UserSystemFramework.Scripts.System.Utilities;
using Random = UnityEngine.Random;

namespace UserSystemFramework.Scripts.System.Controllers.Classes
{
    
    [DependsOn(Controller = typeof(ServiceConfigurationsController))]
    [DependsOn(Controller = typeof(ServiceDependenciesController))]
    public class LocalAccountController : BaseController<LocalAccountController>, IController
    {
        protected ServerRequestSenderService ServerRequest { private set; get; }
        private const string GuestAccountCharList = "qwertyuoipasdfghjklzxcvbnm1234567890";
        public static AccountStatus CurrentAccountType { set; get; }
        public static IUser CurrentLocalUser { get; set; }
        public static string LastUserName
        {
            get => PlayerPrefs.GetString("LastUserName").ToDecrypted();
            internal set => PlayerPrefs.SetString("LastUserName", value.ToEncrypted());
        }

        public static string LastPassword
        {
            get => PlayerPrefs.GetString("LastPassword").ToDecrypted();
            internal set => PlayerPrefs.SetString("LastPassword", value.ToEncrypted());
        }
        private void OnEnable() => EventPublisher.OnServerInitialized += ServerInitializedEvent;
        private void OnDisable() => EventPublisher.OnServerInitialized -= ServerInitializedEvent;

        [ControllerInitialization]
        public override IEnumerator InitializeController()
        {
            yield return base.InitializeController();
            yield return new WaitForControllers<LocalAccountController>();
            ServerRequest = Structure.Classes.ServiceHandler.Locator.Get<ServerRequestSenderService>();
            CurrentLocalUser = new UserData();
            CurrentLocalUser.CustomUserData = ConfigService.CustomFieldsConfig.customUserFields;
            CurrentAccountType = AccountStatus.NotLoggedIn;            
            CompleteInitialization();
        }

        private void ServerInitializedEvent()
        {
            if (ConfigService.ServerConfig.autoLoginOrRegister)
            {
                if (String.IsNullOrEmpty(LastUserName)) { Register(); }
                else { Login(); }
            }
        }

        private async void Register()
        {
            LastUserName = GenerateUserName();
            LastPassword = GenerateComplexPassword();
            CurrentLocalUser.Password = LastPassword;
            CurrentLocalUser.UserName = LastUserName;
            await AwaitableWaitUntil.WaitUntil(()=> ServerRequest.IsReady);
            await ServerRequest.SendRequest(ServerRequestGetterService.Get(RequestType.RegisterRequest), RegisterCallback);
        }

        internal async void RegisterManual(IUser user)
        {
            CurrentLocalUser = user;
            await AwaitableWaitUntil.WaitUntil(()=> ServerRequest.IsReady);
            await ServerRequest.SendRequest(ServerRequestGetterService.Get(RequestType.RegisterRequest), RegisterCallback);
        }

        private async void Login()
        {
            CurrentLocalUser.Password = LastPassword;
            CurrentLocalUser.UserName = LastUserName;
            await AwaitableWaitUntil.WaitUntil(()=> ServerRequest.IsReady);
            await ServerRequest.SendRequest(ServerRequestGetterService.Get(RequestType.LoginRequest), LoginCallback);
        }
        internal async void LoginManual(IUser user)
        {
            CurrentLocalUser = user;
            await AwaitableWaitUntil.WaitUntil(()=> ServerRequest.IsReady);
            await ServerRequest.SendRequest(ServerRequestGetterService.Get(RequestType.LoginRequest), LoginCallback);
        }

        internal void Logout(IRequest completedRequest, bool restartCurrentScene = false)
        {
            CurrentLocalUser = new UserData();
            CurrentLocalUser.IsLoggedIn = false;
            CurrentLocalUser.CustomUserData = ConfigService.CustomFieldsConfig.customUserFields;
            EventPublisher.TriggerLogoutComplete(completedRequest);
            if (restartCurrentScene)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        internal async void DeleteUserData(IUser user)
        {
            CurrentLocalUser = user;
            await AwaitableWaitUntil.WaitUntil(()=> ServerRequest.IsReady);
            await ServerRequest.SendRequest(ServerRequestGetterService.Get(RequestType.DeleteAccountRequest), RegisterCallback);
        }

        private void RegisterCallback(IRequest resultingRequest)
        {
            switch (resultingRequest.ResultType)
            {
                case RequestResultType.Success:
                    LoginCallback(resultingRequest);
                    if(!CurrentLocalUser.UserName.Contains(ConfigService.ServerConfig.guestAccountPrefix))
                        EventPublisher.TriggerRealAccountRegistered(resultingRequest);
                    else
                        EventPublisher.TriggerGuestAccountRegistered(resultingRequest);
                    break;
                case RequestResultType.Fail:
                case RequestResultType.Neutral:
                case RequestResultType.Undefined:
                    LastUserName = "";
                    LastPassword = "";
                    DebugService.Log("Registration failed" + resultingRequest.ConnectionResultText, DebuggingLevel.AllDebug);
                    EventPublisher.TriggerLoginErrorFired(resultingRequest);
                    break;
            }
        }

        private void LoginCallback(IRequest resultingRequest)
        {
            switch (resultingRequest.ResultType)
            {
                case RequestResultType.Success:
                    string[] splitResult = resultingRequest.ConnectionResponseHeaders["RequestResult"].Split('|');
                    CurrentLocalUser.IsLoggedIn = true;
                    CurrentLocalUser.ID = splitResult[1].ToInt();
                    CurrentLocalUser.IsGuestAccount = LastUserName.Contains($"{ConfigService.ServerConfig.guestAccountPrefix}-");
                    CurrentLocalUser.CopyTokenFromRequest(resultingRequest);
                    CurrentAccountType = CurrentLocalUser.IsGuestAccount ? AccountStatus.Guest : AccountStatus.Registered;
                    DebugService.LogImportant("Successfully logged the user in ID:" + CurrentLocalUser.ID, DebuggingLevel.VeryImportantOnly);
                    //DebugService.LogTest(splitResult[2], DebuggingLevel.AllDebug);
                    CurrentLocalUser.DeserializeFromJson(splitResult[2]);
                    CurrentLocalUser.IsLoggedIn = true;
                    EventPublisher.TriggerLoginComplete(resultingRequest);
                    FindObjectsOfType<MonoBehaviour>().OfType<IComponentUpdater>().ToList().ForEach(x => x.InitializeComponentUpdater());
                    break;
                case RequestResultType.Fail:
                case RequestResultType.Neutral:
                case RequestResultType.Undefined:
                    LastUserName = "";
                    LastPassword = "";
                    DebugService.Log("Login failed" + resultingRequest.ConnectionResultText, DebuggingLevel.AllDebug);
                    EventPublisher.TriggerLoginErrorFired(resultingRequest);
                    break;
            }
        }

        public static string GenerateUserName()
        {
            string randomString = "";
            for (int i = 0; i < 8; i++)
            {
                randomString += GuestAccountCharList[Random.Range(0, GuestAccountCharList.Length)];
            }
            return $"{ConfigService.ServerConfig.guestAccountPrefix}-" + randomString;
        }

        public static string GenerateComplexPassword()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] randomNumber = new byte[32];
            rng.GetBytes(randomNumber);
            string password = Convert.ToBase64String(randomNumber);
            return password;
        }
    }
}