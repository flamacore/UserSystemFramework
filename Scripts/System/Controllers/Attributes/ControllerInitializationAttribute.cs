using System;
using System.Reflection;

namespace UserSystemFramework.Scripts.System.Controllers.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ControllerInitializationAttribute : Attribute { }
}