using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class GetContactsApiRequest : BaseApiRequest
    {
        private IUser _userData;
        public GetContactsApiRequest(IUser userData) => _userData = userData;
        public override void Call()
        {
            ContactSystemController.Instance.GetContacts(APIRequest, _userData.ID);
            EventPublisher.OnGetContactsComplete += SuccessCallback;
            EventPublisher.OnGetContactsErrorFired += FailureCallback;
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnGetContactsComplete -= SuccessCallback;
            EventPublisher.OnGetContactsErrorFired -= FailureCallback;
            _userData = null;
        }
    }
}