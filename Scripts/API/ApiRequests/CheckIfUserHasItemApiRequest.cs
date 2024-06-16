using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class CheckIfUserHasItemApiRequest : BaseApiRequest
    {
        private readonly IUser _user;
        private readonly int _itemId;
        private readonly int _quantity;

        public CheckIfUserHasItemApiRequest(IUser user, int itemId, int quantity = 1)
        {
            _user = user;
            _itemId = itemId;
            _quantity = quantity;
        }

        public override void Call()
        {
            ItemSystemController.Instance.CheckIfUserHasItem(_user, _itemId, _quantity);
            EventPublisher.OnCheckIfUserHasItem += SuccessCallback;
            EventPublisher.OnCheckIfUserHasItemErrorFired += FailureCallback;
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnCheckIfUserHasItem -= SuccessCallback;
            EventPublisher.OnCheckIfUserHasItemErrorFired -= FailureCallback;
        }
    }
}