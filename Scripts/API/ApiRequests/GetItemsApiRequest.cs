using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class GetItemsApiRequest : BaseApiRequest
    {
        public GetItemsApiRequest() { }
        public override void Call()
        {
            ItemSystemController.Instance.GetItems();
            EventPublisher.OnGetItems += SuccessCallback;
            EventPublisher.OnGetItemsErrorFired += FailureCallback;
        }
        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnGetItems -= SuccessCallback;
            EventPublisher.OnGetItemsErrorFired -= FailureCallback;
        }
    }
}