using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Enums;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    /// <summary>
    ///  This class is used to send a friend request to another user.
    ///  It will call the ChangeContactRelationship method of the ContactSystemController.
    ///  It will also subscribe to the OnChangeContactRelationshipComplete and OnChangeContactRelationshipErrorFired events of the EventPublisher.
    ///  It will unsubscribe from the events when the ReleaseUnmanagedResources method is called.
    ///  It will call the SuccessCallback method when the OnChangeContactRelationshipComplete event is fired.
    ///  It will call the FailureCallback method when the OnChangeContactRelationshipErrorFired event is fired.
    ///  It will call the ReleaseUnmanagedResources method when the SuccessCallback or FailureCallback method is called.
    /// </summary>
    public class AddContactApiRequest : BaseApiRequest
    {
        private IUser _requestedContact;
        private IUser _requestingUser;
        public AddContactApiRequest(IUser requestedContact, IUser requestingUser = null)
        {
            _requestedContact = requestedContact;
            _requestingUser = requestingUser ?? LocalAccountController.CurrentLocalUser;
        }
        public override void Call()
        {
            ContactSystemController.Instance.ChangeContactRelationship(_requestedContact.ID, (int) ContactStatus.FriendRequestSent, _requestingUser.ID);
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