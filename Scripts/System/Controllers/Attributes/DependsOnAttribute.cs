// ReSharper disable SuspiciousTypeConversion.Global

using System;
using System.Collections.Generic;
using UserSystemFramework.Scripts.System.Controllers.Interfaces;

namespace UserSystemFramework.Scripts.System.Controllers.Attributes
{
    /// <summary>
    /// This is an attribute designed to keep consistency and easier sorting between controllers. Any controller can depend on each other but of course,
    /// 
    /// </summary>
    [AttributeUsage( AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute
    {
        public Type Controller;
        public static List<IController> GetControllerDependencies<T>() where T : IController
        {
            DependsOnAttribute[] allDependsOnAttributes =
                (DependsOnAttribute[]) GetCustomAttributes(typeof(T), typeof(DependsOnAttribute));
            List<IController> controllers = new List<IController>();
            foreach (DependsOnAttribute dependsOnAttribute in allDependsOnAttributes)
            {
                if(!typeof(IController).IsAssignableFrom(dependsOnAttribute.Controller))
                {
                    throw new SystemException("DependsOn attribute used on a class which does not inherit from IController or BaseController<T>.");
                }
                controllers.Add(dependsOnAttribute.Controller as IController);
            }
            return controllers;
        }
    }
}