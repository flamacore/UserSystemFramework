using UserSystemFramework.Scripts.System.Controllers.Attributes;
using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;
using UserSystemFramework.Scripts.System.Utilities;

namespace UserSystemFramework.Scripts.System.Services.Classes
{
    public class HeartbeatService : Service
    {
        public override ServicePriority Priority => ServicePriority.Low;
        public static int HeartbeatRequestCount = 0;
        private IRequest _heartbeatRequest;
        protected ServerRequestSenderService ServerRequest { private set; get; }
        public override void Init()
        {
            base.Init();
            HeartbeatRequestCount = 0;
            ServerRequest = Structure.Classes.ServiceHandler.Locator.Get<ServerRequestSenderService>();
            EventPublisher.OnLoginComplete += TriggerHeartbeatCycle;
        }

        private void TriggerHeartbeatCycle(IRequest completedRequest)
        {
            _heartbeatRequest = ServerRequestGetterService.Get(RequestType.HeartbeatRequest);
            if (_heartbeatRequest != null)
            {
                _heartbeatRequest.CopyTokenFromRequest(completedRequest);
                HeartbeatCycle(_heartbeatRequest);
            }
        }

        [RecurringTask]
        private async void HeartbeatCycle(IRequest request)
        {
            if (request.ResultType == RequestResultType.Fail)
            {
                DebugService.LogError("Heartbeat Error. Terminating connection.", DebuggingLevel.ErrorsOnly);
                EventPublisher.TriggerHeartbeatError();
            }
            else
            {
                DebugService.Log("Calling heartbeat", DebuggingLevel.AllDebug);
                if (!_heartbeatRequest.ConnectionParameters.ContainsKey("userName"))
                {
                    _heartbeatRequest.ConnectionParameters.Add("userName", LocalAccountController.CurrentLocalUser.UserName);
                }
                _heartbeatRequest = ServerRequestGetterService.Get(RequestType.HeartbeatRequest);
                _heartbeatRequest = await ServerRequest.SendRequest(_heartbeatRequest, HeartbeatCycle, ConfigService.ServerConfig.heartbeatInterval);
                EventPublisher.TriggerHeartbeat();
                HeartbeatRequestCount++;
            }
        }
    }
}