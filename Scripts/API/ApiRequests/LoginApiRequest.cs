using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class LoginApiRequest : BaseApiRequest
    {
        private IUser _userData;
        public LoginApiRequest(IUser userData) => _userData = userData;
        public override void Call()
        {
            LocalAccountController.Instance.LoginManual(_userData);
            EventPublisher.OnLoginComplete += SuccessCallback;
            EventPublisher.OnLoginErrorFired += FailureCallback;
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnLoginComplete -= SuccessCallback;
            EventPublisher.OnLoginErrorFired -= FailureCallback;
            _userData = null;
        }
    }
}