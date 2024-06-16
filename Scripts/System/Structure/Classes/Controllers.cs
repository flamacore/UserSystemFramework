using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UserSystemFramework.Scripts.System.Controllers.Attributes;
using UserSystemFramework.Scripts.System.Controllers.Interfaces;
using Object = UnityEngine.Object;

namespace UserSystemFramework.Scripts.System.Structure.Classes
{
    public static class Controllers
    {
        private static List<IController> _allControllers;
        public static bool ControllersReady = false;
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void FetchControllersFromScene()
        {
            _allControllers = Object.FindObjectsOfType<MonoBehaviour>().OfType<IController>().ToList();
            ControllersReady = true;
        }

        public static IController Get<TController>()
        {
            if (ControllersReady)
                return _allControllers.First(x => x.GetType() == typeof(TController));
            else
                throw new SystemException("Controllers are not ready!");
        }
    }
    public class WaitForControllerGetter : CustomYieldInstruction
    {
        public override bool keepWaiting => !Controllers.ControllersReady;
    }

    class WaitWhile1 : CustomYieldInstruction
    {
        Func<bool> predicate;
        public override bool keepWaiting => predicate();
        public WaitWhile1(Func<bool> predicate) { this.predicate = predicate; }
    }
}