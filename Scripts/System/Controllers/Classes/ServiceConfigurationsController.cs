using System.Collections;
using UnityEngine;
using UserSystemFramework.Scripts.System.Config;
using UserSystemFramework.Scripts.System.Controllers.Attributes;
using UserSystemFramework.Scripts.System.Controllers.Interfaces;
using UserSystemFramework.Scripts.System.Services.Classes;

namespace UserSystemFramework.Scripts.System.Controllers.Classes
{
    /// <summary>
    /// This controller gets references for config files and attaches them to the corresponding services.
    /// </summary>
    public class ServiceConfigurationsController : BaseController<ServiceConfigurationsController>, IController
    {
        //Config references
        [SerializeField] private ServerConfiguration serverConfig;
        [SerializeField] private ProjectConfiguration projectConfig;
        [SerializeField] private CustomFieldsConfiguration customFieldsConfig;
        [ControllerInitialization]
        public override IEnumerator InitializeController()
        {
            yield return base.InitializeController();
            ConfigService.ProjectConfig = projectConfig;
            ConfigService.ServerConfig = serverConfig;
            ConfigService.CustomFieldsConfig = customFieldsConfig;
            DebugService.Log("Service configurations have been assigned.", DebuggingLevel.Everything);    
            DontDestroyOnLoad(this.gameObject);
            CompleteInitialization();
        }
    }
}