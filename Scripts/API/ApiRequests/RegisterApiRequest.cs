using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class RegisterApiRequest : BaseApiRequest
    {
        private IUser _userData;
        public RegisterApiRequest(IUser userData) => _userData = userData;
        public override void Call()
        {
            LocalAccountController.Instance.RegisterManual(_userData);
            EventPublisher.OnRealAccountRegistered += SuccessCallback;
            EventPublisher.OnGuestAccountRegistered += SuccessCallback;
            EventPublisher.OnLoginErrorFired += FailureCallback;
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnRealAccountRegistered -= SuccessCallback;
            EventPublisher.OnGuestAccountRegistered -= SuccessCallback;
            EventPublisher.OnLoginErrorFired -= FailureCallback;
            _userData = null;
        }
    }
}