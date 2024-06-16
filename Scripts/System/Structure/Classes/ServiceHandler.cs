using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Structure.Classes
{
    public class ServiceHandler
    {
        private ServiceHandler() { }
        private readonly Dictionary<string, IService> _serviceDictionary = new Dictionary<string, IService>();
        public static ServiceHandler Locator { get; private set; }
        public static void Initialize()
        {
            Locator = new ServiceHandler();
        }
        
        public T Get<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!_serviceDictionary.ContainsKey(key))
            {
                Debug.LogError($"{key} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }
            return (T)_serviceDictionary[key];
        }
        
        public Dictionary<string, IService> GetAll() => _serviceDictionary;
        public List<IService> GetListWithPriority(ServicePriority priority) => _serviceDictionary.Values.Where(x=> x.Priority == priority).ToList();
        
        public void Register<T>(T service) where T : IService
        {
            string key = typeof(T).Name;
            if (_serviceDictionary.ContainsKey(key))
            {
                Debug.LogError($"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
                return;
            }
            _serviceDictionary.Add(key, service);
        }
        
        public void Unregister<T>() where T : IService
        {
            string key = typeof(T).Name;
            if (!_serviceDictionary.ContainsKey(key))
            {
                Debug.LogError($"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
                return;
            }
            _serviceDictionary.Remove(key);
        }

        public async void InitializeServices()
        {
            foreach (IService service in GetListWithPriority(ServicePriority.High)) { service.Init(); }
            await Task.Delay(100);
            foreach (IService service in GetListWithPriority(ServicePriority.Medium)) { service.Init(); }
            await Task.Delay(100);
            foreach (IService service in GetListWithPriority(ServicePriority.Low)) { service.Init(); }
            await Task.Delay(100);
            foreach (IService service in GetListWithPriority(ServicePriority.VeryLow)) { service.Init(); }
            await Task.Delay(100);
        }

        public async void DisableServices()
        {
            foreach (IService service in GetListWithPriority(ServicePriority.High)) { service.OnDisable(); }
            await Task.Delay(100);
            foreach (IService service in GetListWithPriority(ServicePriority.Medium)) { service.OnDisable(); }
            await Task.Delay(100);
            foreach (IService service in GetListWithPriority(ServicePriority.Low)) { service.OnDisable(); }
            await Task.Delay(100);
            foreach (IService service in GetListWithPriority(ServicePriority.VeryLow)) { service.OnDisable(); }
            await Task.Delay(100);
        }
    }

    public class WaitForServices : CustomYieldInstruction
    {
        public override bool keepWaiting
        {
            get
            {
                bool servicesReady = true;
                foreach (KeyValuePair<string,IService> service in ServiceHandler.Locator.GetAll())
                {
                    if(!service.Value.IsReady)
                        servicesReady = false;
                }
                return servicesReady;
            }
        }
    }
    
    public class WaitForService : CustomYieldInstruction
    {
        private IService _service;
        public WaitForService(IService service)
        {
            _service = service;
        }
        public override bool keepWaiting => _service.IsReady;
    }
}