using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UserSystemFramework.Scripts.System.Controllers.Attributes;
using UserSystemFramework.Scripts.System.Controllers.Interfaces;
using UserSystemFramework.Scripts.System.Services.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;
using UserSystemFramework.Scripts.System.Utilities;

namespace UserSystemFramework.Scripts.System.Controllers.Classes
{
    [Serializable]
    public abstract class BaseController<T> : MonoBehaviourSingleton<T> where T : Component, IController
    {
        public virtual bool ControllerInitComplete { get; set; }
        
        public virtual void Start()
        {
            StartCoroutine(InitializeController());
        }

        public virtual IEnumerator InitializeController()
        {
            ControllerInitComplete = false;
            yield return new WaitForServices();
            yield return new WaitForControllerGetter();
        }
        public virtual void CompleteInitialization()
        {
            ControllerInitComplete = true;
            DebugService.Log($"Controller initialization complete: {GetType().Name}", DebuggingLevel.AllDebug);
        }
    }
    public class WaitForControllers<T> : CustomYieldInstruction where T: IController
    {
        private readonly List<IController> _waitList;
        public WaitForControllers()
        {
            _waitList = DependsOnAttribute.GetControllerDependencies<T>();
        }
        public override bool keepWaiting
        {
            get
            {
                bool controllersReady = true;
                foreach (IController controller in _waitList)
                {
                    if(controller == null || !controller.ControllerInitComplete)
                        controllersReady = false;
                }
                return controllersReady;
            }
        }
    }
}