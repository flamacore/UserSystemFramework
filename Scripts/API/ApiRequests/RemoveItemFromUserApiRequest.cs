using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class RemoveItemFromUserApiRequest : BaseApiRequest
    {
        private readonly IUser _user;
        private readonly int _itemId;
        private readonly int _quantity;

        public RemoveItemFromUserApiRequest(IUser user, int itemId, int quantity = 1)
        {
            _user = user;
            _itemId = itemId;
            _quantity = quantity;
        }

        public override void Call()
        {
            ItemSystemController.Instance.RemoveItemFromUser(_user, _itemId, _quantity);
            EventPublisher.OnRemoveItemFromUser += SuccessCallback;
            EventPublisher.OnRemoveItemFromUserErrorFired += FailureCallback;
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnRemoveItemFromUser -= SuccessCallback;
            EventPublisher.OnRemoveItemFromUserErrorFired -= FailureCallback;
        }
    }
}