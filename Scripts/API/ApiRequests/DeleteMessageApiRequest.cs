using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class DeleteMessageApiRequest : BaseApiRequest
    {
        private int _messageId;

        public DeleteMessageApiRequest(int messageId)
        {
            _messageId = messageId;
        }
        public override void Call()
        {
            UserMessageSystemController.Instance.DeleteMessage(_messageId);
            EventPublisher.OnDeleteMessage += SuccessCallback;
            EventPublisher.OnDeleteMessageErrorFired += FailureCallback;
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnDeleteMessage -= SuccessCallback;
            EventPublisher.OnDeleteMessageErrorFired -= FailureCallback;
        }
    }
}