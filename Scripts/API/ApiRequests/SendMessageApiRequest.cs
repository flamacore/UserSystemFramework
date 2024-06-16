using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class SendMessageApiRequest : BaseApiRequest
    {
        private IUser Sender => LocalAccountController.CurrentLocalUser;
        private readonly IUser _receiver;
        private readonly string _message;

        public SendMessageApiRequest(IUser receiver, string message)
        {
            _receiver = receiver;
            _message = message;
        }

        public override void Call()
        {
            UserMessageSystemController.Instance.SendUserMessage(Sender, _receiver, _message);
            EventPublisher.OnSendMessageComplete += SuccessCallback;
            EventPublisher.OnSendMessageCompleteErrorFired += FailureCallback;
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnSendMessageComplete -= SuccessCallback;
            EventPublisher.OnSendMessageCompleteErrorFired -= FailureCallback;
        }
    }
}