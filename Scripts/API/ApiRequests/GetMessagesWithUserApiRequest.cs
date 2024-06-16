using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class GetMessagesWithUserApiRequest : BaseApiRequest
    {
        private int _page;
        private int _limit;
        private IUser _user;
        public GetMessagesWithUserApiRequest(IUser conversant, int page, int limit)
        {
            _page = page;
            _limit = limit;
            _user = conversant;
        }
        public override void Call()
        {
            UserMessageSystemController.Instance.GetAllMessagesWithUser(_user, _page, _limit);
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