using System.Collections;
using System.Collections.Generic;
using UserSystemFramework.Scripts.System.Controllers.Interfaces;
using UserSystemFramework.Scripts.System.Controllers.Attributes;
using UserSystemFramework.Scripts.System.Services.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Controllers.Classes
{
    public class TradeSystemController : BaseController<TradeSystemController>, IController
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
        
        internal async void AddItemToTrade(int itemId, int amount, int cost)
        {
            IRequest request = ServerRequestGetterService.Get(RequestType.AddItemToTradeRequest, true, false, new Dictionary<string, string>()
            {
                {"itemId", itemId.ToString()},
                {"amount", amount.ToString()},
                {"cost", cost.ToString()}
            });
            await ServerRequest.SendRequest(request, AddItemToTradeCallback);
        }
        
        [RequestCallbackMethod]
        private void AddItemToTradeCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Neutral:
                    EventPublisher.TriggerAddItemToTrade(returnRequest);
                    break;
                case RequestResultType.Fail:
                    EventPublisher.TriggerAddItemToTradeErrorFired(returnRequest);
                    break;
                case RequestResultType.Success:
                    EventPublisher.TriggerAddItemToTrade(returnRequest);
                    break;
                case RequestResultType.Undefined:
                    EventPublisher.TriggerAddItemToTradeErrorFired(returnRequest);
                    break;
            }
        }
        
        internal async void RemoveItemFromTrade(int itemId, int amount)
        {
            IRequest request = ServerRequestGetterService.Get(RequestType.RemoveItemFromTradeRequest, true, false, new Dictionary<string, string>()
            {
                {"itemId", itemId.ToString()},
                {"amount", amount.ToString()}
            });
            await ServerRequest.SendRequest(request, RemoveItemFromTradeCallback);
        }
        
        [RequestCallbackMethod]
        private void RemoveItemFromTradeCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Neutral:
                    EventPublisher.TriggerRemoveItemFromTrade(returnRequest);
                    break;
                case RequestResultType.Fail:
                    EventPublisher.TriggerRemoveItemFromTradeErrorFired(returnRequest);
                    break;
                case RequestResultType.Success:
                    EventPublisher.TriggerRemoveItemFromTrade(returnRequest);
                    break;
                case RequestResultType.Undefined:
                    EventPublisher.TriggerRemoveItemFromTradeErrorFired(returnRequest);
                    break;
            }
        }
        
        internal async void GetTradeInventory()
        {
            IRequest request = ServerRequestGetterService.Get(RequestType.GetTradeInventoryRequest);
            await ServerRequest.SendRequest(request, GetTradeInventoryCallback);
        }
        
        [RequestCallbackMethod]
        private void GetTradeInventoryCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Neutral:
                    EventPublisher.TriggerGetTradeInventory(returnRequest);
                    break;
                case RequestResultType.Fail:
                    EventPublisher.TriggerGetTradeInventoryErrorFired(returnRequest);
                    break;
                case RequestResultType.Success:
                    EventPublisher.TriggerGetTradeInventory(returnRequest);
                    break;
                case RequestResultType.Undefined:
                    EventPublisher.TriggerGetTradeInventoryErrorFired(returnRequest);
                    break;
            }
        }
    }
}