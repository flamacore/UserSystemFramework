using UnityEngine;
using UserSystemFramework.Scripts.System.Services.Classes;

namespace UserSystemFramework.Scripts.System.Structure.Classes
{
    public static class ServiceBootstrap
    {
        public static bool IsGameRunning;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            IsGameRunning = true;
            EventPublisher.TriggerServicesBeginInitializing();
            ServiceHandler.Initialize();
            ServiceHandler.Locator.Register(new UtilityService());
            ServiceHandler.Locator.Register(new DebugService());
            ServiceHandler.Locator.Register(new HeartbeatService());
            ServiceHandler.Locator.Register(new ServerRequestSenderService());
            ServiceHandler.Locator.Register(new ServerRequestGetterService());
            ServiceHandler.Locator.Register(new TimeService());
            ServiceHandler.Locator.Register(new ConfigService());
            ServiceHandler.Locator.InitializeServices();
            EventPublisher.TriggerServicesInitialized();
        }
    }
}