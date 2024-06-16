using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UserSystemFramework.Scripts.System.Controllers.Attributes;
using UserSystemFramework.Scripts.System.Controllers.Interfaces;
using UserSystemFramework.Scripts.System.Data.Classes;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Services.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;
using UserSystemFramework.Scripts.System.Utilities;

namespace UserSystemFramework.Scripts.System.Controllers.Classes
{
    [DependsOn(Controller = typeof(LocalAccountController))]
    public class UserMessageSystemController : BaseController<UserMessageSystemController>, IController
    {
        protected ServerRequestSenderService ServerRequest { private set; get; }
        
        [ControllerInitialization]
        public override IEnumerator InitializeController()
        {
            yield return base.InitializeController();
            yield return new WaitForControllers<AchievementSystemController>();
            ServerRequest = ServiceHandler.Locator.Get<ServerRequestSenderService>();
            CompleteInitialization();
        }
        internal async void SendUserMessage(IUser sender, IUser receiver, string message)
        {
            IRequest request = ServerRequestGetterService.Get(RequestType.SendMessageRequest, true, false, new Dictionary<string, string>()
            {
                {"fromUserId", sender.ID.ToString()},
                {"toUserId", receiver.ID.ToString()},
                {"message", message}
            });
            await ServerRequest.SendRequest(request, SendUserMessageCallback);
        }

        [RequestCallbackMethod]
        private void SendUserMessageCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Neutral:
                    EventPublisher.TriggerSendMessageComplete(returnRequest);
                    break;
                case RequestResultType.Fail:
                    EventPublisher.TriggerSendMessageCompleteErrorFired(returnRequest);
                    break;
                case RequestResultType.Success:
                    EventPublisher.TriggerSendMessageComplete(returnRequest);
                    break;
                case RequestResultType.Undefined:
                    EventPublisher.TriggerSendMessageCompleteErrorFired(returnRequest);
                    break;
            }
        }
        
        internal async void GetAllMessages(int page = 0, int limit = 5000)
        {
            IRequest request = ServerRequestGetterService.Get(RequestType.GetMessagesRequest, true, false, new Dictionary<string, string>()
            {
                {"forUserId", LocalAccountController.CurrentLocalUser.ID.ToString()},
                {"page", page.ToString()},
                {"limit", limit.ToString()}
            });
            await ServerRequest.SendRequest(request, CheckAllMessagesForUserCallback);
        }

        [RequestCallbackMethod]
        private void CheckAllMessagesForUserCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Neutral:
                    EventPublisher.TriggerGetMessages(returnRequest);
                    break;
                case RequestResultType.Fail:
                    EventPublisher.TriggerGetMessagesErrorFired(returnRequest);
                    break;
                case RequestResultType.Success:
                    List<MessageData> messages = new List<MessageData>(JsonConvert.DeserializeObject<List<MessageData>>(returnRequest.ConnectionResponseHeaders["RequestResult"])!);
                    LocalAccountController.CurrentLocalUser.LoadedMessages = messages;
                    EventPublisher.TriggerGetMessages(returnRequest);
                    break;
                case RequestResultType.Undefined:
                    EventPublisher.TriggerGetMessagesErrorFired(returnRequest);
                    break;
            }
        }
        
        internal async void GetAllMessagesWithUser(IUser conversant, int page = 0, int limit = 5000)
        {
            IRequest request = ServerRequestGetterService.Get(RequestType.GetMessagesWithUserRequest, true, false, new Dictionary<string, string>()
            {
                {"toUserId", conversant.ID.ToString()},
                {"fromUserId", LocalAccountController.CurrentLocalUser.ID.ToString()},
                {"page", page.ToString()},
                {"limit", limit.ToString()}
            });
            await ServerRequest.SendRequest(request, GetAllMessagesWithUserCallback);
        }

        [RequestCallbackMethod]
        private void GetAllMessagesWithUserCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Neutral:
                    EventPublisher.TriggerGetMessagesWithUser(returnRequest);
                    break;
                case RequestResultType.Fail:
                    EventPublisher.TriggerGetMessagesWithUserErrorFired(returnRequest);
                    break;
                case RequestResultType.Success:
                    List<MessageData> messages = new List<MessageData>(JsonConvert.DeserializeObject<List<MessageData>>(returnRequest.ConnectionResponseHeaders["RequestResultMessages"])!);
                    if (LocalAccountController.CurrentLocalUser.LoadedConversations == null)
                        LocalAccountController.CurrentLocalUser.LoadedConversations = new List<Conversation>();
                    LocalAccountController.CurrentLocalUser.LoadedConversations.Add(
                        new Conversation(
                            LocalAccountController.CurrentLocalUser,
                            JsonConvert.DeserializeObject<UserData>(
                                returnRequest.ConnectionResponseHeaders["RequestResultOtherUser"]
                            ),
                            messages
                        )
                    );
                    EventPublisher.TriggerGetMessagesWithUser(returnRequest);
                    break;
                case RequestResultType.Undefined:
                    EventPublisher.TriggerGetMessagesWithUserErrorFired(returnRequest);
                    break;
            }
        }
        
        internal async void ReadMessage(int messageId)
        {
            IRequest request = ServerRequestGetterService.Get(RequestType.ReadMessageRequest, true, false, new Dictionary<string, string>()
            {
                {"messageId", messageId.ToString()}
            });
            await ServerRequest.SendRequest(request, ReadMessageCallback);
        }

        [RequestCallbackMethod]
        private void ReadMessageCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Neutral:
                    EventPublisher.TriggerReadMessage(returnRequest);
                    break;
                case RequestResultType.Fail:
                    EventPublisher.TriggerReadMessageErrorFired(returnRequest);
                    break;
                case RequestResultType.Success:
                    EventPublisher.TriggerReadMessage(returnRequest);
                    break;
                case RequestResultType.Undefined:
                    EventPublisher.TriggerReadMessageErrorFired(returnRequest);
                    break;
            }
        }
        
        internal async void DeleteMessage(int messageId)
        {
            IRequest request = ServerRequestGetterService.Get(RequestType.DeleteMessageRequest, true, false, new Dictionary<string, string>()
            {
                {"messageId", messageId.ToString()}
            });
            await ServerRequest.SendRequest(request, DeleteMessageCallback);
        }

        [RequestCallbackMethod]
        private void DeleteMessageCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Neutral:
                    EventPublisher.TriggerDeleteMessage(returnRequest);
                    break;
                case RequestResultType.Fail:
                    EventPublisher.TriggerDeleteMessageErrorFired(returnRequest);
                    break;
                case RequestResultType.Success:
                    EventPublisher.TriggerDeleteMessage(returnRequest);
                    break;
                case RequestResultType.Undefined:
                    EventPublisher.TriggerDeleteMessageErrorFired(returnRequest);
                    break;
            }
        }
    }
}