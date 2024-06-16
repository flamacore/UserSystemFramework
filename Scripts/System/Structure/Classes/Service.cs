using UserSystemFramework.Scripts.System.Services.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Structure.Classes
{
    public class Service : IService
    {
        public delegate void InitializeComplete();
        public event InitializeComplete OnInit;
        public delegate void SceneReady();
        public event SceneReady OnSceneReady;
        public UtilityService UtilityService { get; private set; }

        public virtual void Init()
        {
            UtilityService = ServiceHandler.Locator.Get<UtilityService>();
            OnInit?.Invoke();
            IsReady = true;
        }

        public virtual void OnDisable() { }
        public void TriggerSceneReady()
        {
            OnSceneReady?.Invoke();
        }

        public virtual ServicePriority Priority => ServicePriority.VeryLow;
        public bool IsReady { get; set; }
    }
}