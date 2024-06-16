using System;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.API.Interfaces
{
    public interface IApiRequest : IDisposable
    {
        public void Call();
        public void SuccessCallback(IRequest completedRequest);
        public void FailureCallback(IRequest completedRequest);
        public delegate void OnSuccessCallback(IRequest completedRequest);
        public event OnSuccessCallback SuccessCallbackEvent;
        public delegate void OnFailureCallback(IRequest completedRequest);
        public event OnFailureCallback FailureCallbackEvent;
        public IRequest APIRequest { get; }
        public void ReleaseUnmanagedResources();
    }
}