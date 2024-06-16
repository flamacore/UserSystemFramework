using System;
using UserSystemFramework.Scripts.API.Interfaces;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class BaseApiRequest : IApiRequest
    {
        public IRequest APIRequest
        {
            get => _apiRequest;
            internal set => _apiRequest = value;
        }
        private IRequest _apiRequest;
        public event IApiRequest.OnSuccessCallback SuccessCallbackEvent;
        public event IApiRequest.OnFailureCallback FailureCallbackEvent;
        public bool CallbackFired = false;
        public virtual void Call() {}

        public virtual void SuccessCallback(IRequest completedRequest)
        {
            CallbackFired = true;
            _apiRequest = completedRequest;
            SuccessCallbackEvent?.Invoke(completedRequest);
            Dispose();
        }

        public virtual void FailureCallback(IRequest completedRequest)
        {
            CallbackFired = true;
            _apiRequest = completedRequest;
            FailureCallbackEvent?.Invoke(completedRequest);
            Dispose();
        }
        public virtual void ReleaseUnmanagedResources() => _apiRequest = null;
        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
        ~BaseApiRequest() => ReleaseUnmanagedResources();
    }
}