using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class ReadMessageApiRequest : BaseApiRequest
    {
        private int _messageId;

        public ReadMessageApiRequest(int messageId)
        {
            _messageId = messageId;
        }
        public override void Call()
        {
            UserMessageSystemController.Instance.ReadMessage(_messageId);
            EventPublisher.OnReadMessage += SuccessCallback;
            EventPublisher.OnReadMessageErrorFired += FailureCallback;
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnReadMessage -= SuccessCallback;
            EventPublisher.OnReadMessageErrorFired -= FailureCallback;
        }
    }
}