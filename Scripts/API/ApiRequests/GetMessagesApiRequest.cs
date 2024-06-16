using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class GetMessagesApiRequest : BaseApiRequest
    {
        private int _page;
        private int _limit;
        public GetMessagesApiRequest(int page, int limit)
        {
            _page = page;
            _limit = limit;
        }
        public override void Call()
        {
            UserMessageSystemController.Instance.GetAllMessages(_page, _limit);
            EventPublisher.OnGetMessages += SuccessCallback;
            EventPublisher.OnGetMessagesErrorFired += FailureCallback;
        }
        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnGetMessages -= SuccessCallback;
            EventPublisher.OnGetMessagesErrorFired -= FailureCallback;
        }
    }
}