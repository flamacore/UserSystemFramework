using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Enums;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class IgnoreContactApiRequest : BaseApiRequest
    {
        private IUser _requestedContact;
        private IUser _requestingUser;
        public IgnoreContactApiRequest(IUser requestedContact, IUser requestingUser = null)
        {
            _requestedContact = requestedContact;
            _requestingUser = requestingUser ?? LocalAccountController.CurrentLocalUser;
        }
        public override void Call()
        {
            ContactSystemController.Instance.ChangeContactRelationship(_requestedContact.ID, (int) ContactStatus.Ignored, _requestingUser.ID);
            EventPublisher.OnChangeContactRelationshipComplete += SuccessCallback;
            EventPublisher.OnChangeContactRelationshipErrorFired += FailureCallback;
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnChangeContactRelationshipComplete -= SuccessCallback;
            EventPublisher.OnChangeContactRelationshipErrorFired -= FailureCallback;
            _requestedContact = null;
            _requestingUser = null;
        }
    }
}