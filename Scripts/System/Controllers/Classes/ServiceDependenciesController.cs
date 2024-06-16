using System.Collections;
using System.Collections.Generic;
using UserSystemFramework.Scripts.System.Controllers.Attributes;
using UserSystemFramework.Scripts.System.Controllers.Interfaces;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Controllers.Classes
{
    [DependsOn(Controller = typeof(ServiceConfigurationsController))]
    public class ServiceDependenciesController : BaseController<ServiceDependenciesController>, IController
    {
        [ControllerInitialization]
        public override IEnumerator InitializeController()
        {
            yield return base.InitializeController();
            yield return new WaitForControllers<ServiceDependenciesController>();
            foreach (KeyValuePair<string, IService> service in Structure.Classes.ServiceHandler.Locator.GetAll())
            {
                service.Value.TriggerSceneReady();
            }
            CompleteInitialization();
        }
        protected override void OnApplicationQuit()
        {
            ServiceBootstrap.IsGameRunning = false;
        }
    }
}