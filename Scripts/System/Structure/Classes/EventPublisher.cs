using System;
using UnityEngine;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Structure.Classes
{
    public static partial class EventPublisher
    {
        //Restricted System Events
        public delegate void ServicesBeginInitializing();
        public static event ServicesBeginInitializing OnServicesBeginInitializing;
        public delegate void ServicesInitialized();
        public static event ServicesInitialized OnServicesInitialized;
        public delegate void ServerInitialized();
        public static event ServerInitialized OnServerInitialized;
        public static void TriggerServerInitialized() => 
            OnServerInitialized?.Invoke();
        public static void TriggerServicesBeginInitializing() => 
            OnServicesBeginInitializing?.Invoke();
        public static void TriggerServicesInitialized() => 
            OnServicesInitialized?.Invoke();
        
        //Heartbeat Events
        public delegate void Heartbeat();
        public static event Heartbeat OnHeartbeat;
        public static void TriggerHeartbeat()
        {
            OnHeartbeat?.Invoke();
        }

        public delegate void HeartbeatError();
        public static event HeartbeatError OnHeartbeatError;
        public static void TriggerHeartbeatError() => OnHeartbeatError?.Invoke();
    }
}