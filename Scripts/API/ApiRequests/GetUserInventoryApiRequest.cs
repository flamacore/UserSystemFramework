using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class GetUserInventoryApiRequest : BaseApiRequest
    {
        public GetUserInventoryApiRequest(IUser user) { }
        public override void Call()
        {
            ItemSystemController.Instance.GetUserInventory();
            EventPublisher.OnGetUserInventory += SuccessCallback;
            EventPublisher.OnGetUserInventoryErrorFired += FailureCallback;
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnGetUserInventory -= SuccessCallback;
            EventPublisher.OnGetUserInventoryErrorFired -= FailureCallback;
        }
    }
}