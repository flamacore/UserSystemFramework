using System;
using System.Reflection;
using UserSystemFramework.Scripts.System.Controllers.Interfaces;
using UserSystemFramework.Scripts.System.Services.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Controllers.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequestCallbackMethodAttribute : Attribute
    {
        public void CallbackDebug(IRequest resultRequest)
        {
            switch (resultRequest.ResultType)
            {
                case RequestResultType.Success:
                    DebugService.Log($"Success Callback: {resultRequest.RequestEnum.ToString()}", DebuggingLevel.Everything);
                    break;
                case RequestResultType.Fail:
                    DebugService.LogError($"Fail Callback: {resultRequest.RequestEnum.ToString()}", DebuggingLevel.ErrorsOnly);
                    break;
                case RequestResultType.Neutral:
                    DebugService.Log($"Neutral Callback: {resultRequest.RequestEnum.ToString()}", DebuggingLevel.Everything);
                    break;
                case RequestResultType.Undefined:
                    DebugService.LogWarning($"Undefined Callback: {resultRequest.RequestEnum.ToString()}", DebuggingLevel.WarningsAndErrors);
                    break;
            }
        }
    }
}