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

namespace UserSystemFramework.Scripts.System.Controllers.Classes
{
    public class ItemSystemController : BaseController<ItemSystemController>, IController
    {
        /// <summary>
        /// The items list. Note that this list is not updated automatically. You need to call GetItems() to update it.
        /// </summary>
        public static List<ItemData> Items = new List<ItemData>();
        protected ServerRequestSenderService ServerRequest { private set; get; }
        
        [ControllerInitialization]
        public override IEnumerator InitializeController()
        {
            yield return base.InitializeController();
            yield return new WaitForControllers<AchievementSystemController>();
            ServerRequest = ServiceHandler.Locator.Get<ServerRequestSenderService>();
            CompleteInitialization();
        }
        
        internal async void GetItems(int page = 0, int limit = 20000)
        {
            IRequest request = ServerRequestGetterService.Get(RequestType.GetItemsRequest, true, false, new Dictionary<string, string>()
            {
                {"page", page.ToString()},
                {"limit", limit.ToString()}
            });
            await ServerRequest.SendRequest(request, GetItemsCallback);
        }
        
        [RequestCallbackMethod]
        private void GetItemsCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Neutral:
                    EventPublisher.TriggerGetItems(returnRequest);
                    break;
                case RequestResultType.Fail:
                    EventPublisher.TriggerGetItemsErrorFired(returnRequest);
                    break;
                case RequestResultType.Success:
                    List<ItemData> items = new List<ItemData>(JsonConvert.DeserializeObject<List<ItemData>>(returnRequest.ConnectionResponseHeaders["RequestResult"])!);
                    Items = items;
                    EventPublisher.TriggerGetItems(returnRequest);
                    break;
                case RequestResultType.Undefined:
                    EventPublisher.TriggerGetItemsErrorFired(returnRequest);
                    break;
            }
        }
        
        internal async void GetUserInventory(int page = 0, int limit = 5000)
        {
            IRequest request = ServerRequestGetterService.Get(RequestType.GetUserInventoryRequest, true, false, new Dictionary<string, string>()
            {
                {"forUserId", LocalAccountController.CurrentLocalUser.ID.ToString()},
                {"page", page.ToString()},
                {"limit", limit.ToString()}
            });
            await ServerRequest.SendRequest(request, GetUserInventoryCallback);
        }

        [RequestCallbackMethod]
        private void GetUserInventoryCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Neutral:
                    EventPublisher.TriggerGetUserInventory(returnRequest);
                    break;
                case RequestResultType.Fail:
                    EventPublisher.TriggerGetUserInventoryErrorFired(returnRequest);
                    break;
                case RequestResultType.Success:
                    List<ItemData> items = new List<ItemData>(JsonConvert.DeserializeObject<List<ItemData>>(returnRequest.ConnectionResponseHeaders["RequestResult"])!);
                    LocalAccountController.CurrentLocalUser.Items = items;
                    EventPublisher.TriggerGetUserInventory(returnRequest);
                    break;
                case RequestResultType.Undefined:
                    EventPublisher.TriggerGetUserInventoryErrorFired(returnRequest);
                    break;
            }
        }
        
        internal async void AddItemToUser(IUser user, int itemId, int quantity)
        {
            IRequest request = ServerRequestGetterService.Get(RequestType.AddItemToUserRequest, true, false, new Dictionary<string, string>()
            {
                {"forUserId", user.ID.ToString()},
                {"itemId", itemId.ToString()},
                {"quantity", quantity.ToString()}
            });
            await ServerRequest.SendRequest(request, AddItemToUserCallback);
        }

        [RequestCallbackMethod]
        private void AddItemToUserCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Neutral:
                    EventPublisher.TriggerAddItemToUser(returnRequest);
                    break;
                case RequestResultType.Fail:
                    EventPublisher.TriggerAddItemToUserErrorFired(returnRequest);
                    break;
                case RequestResultType.Success:
                    EventPublisher.TriggerAddItemToUser(returnRequest);
                    break;
                case RequestResultType.Undefined:
                    EventPublisher.TriggerAddItemToUserErrorFired(returnRequest);
                    break;
            }
        }
        
        internal async void RemoveItemFromUser(IUser user, int itemId, int quantity)
        {
            IRequest request = ServerRequestGetterService.Get(RequestType.RemoveItemFromUserRequest, true, false, new Dictionary<string, string>()
            {
                {"forUserId", user.ID.ToString()},
                {"itemId", itemId.ToString()},
                {"quantity", quantity.ToString()}
            });
            await ServerRequest.SendRequest(request, RemoveItemFromUserCallback);
        }

        [RequestCallbackMethod]
        private void RemoveItemFromUserCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Neutral:
                    EventPublisher.TriggerRemoveItemFromUser(returnRequest);
                    break;
                case RequestResultType.Fail:
                    EventPublisher.TriggerRemoveItemFromUserErrorFired(returnRequest);
                    break;
                case RequestResultType.Success:
                    EventPublisher.TriggerRemoveItemFromUser(returnRequest);
                    break;
                case RequestResultType.Undefined:
                    EventPublisher.TriggerRemoveItemFromUserErrorFired(returnRequest);
                    break;
            }
        }
        
        internal async void CheckIfUserHasItem(IUser user, int itemId, int quantity)
        {
            IRequest request = ServerRequestGetterService.Get(RequestType.CheckIfUserHasItemRequest, true, false, new Dictionary<string, string>()
            {
                {"forUserId", user.ID.ToString()},
                {"itemId", itemId.ToString()},
                {"quantity", quantity.ToString()}
            });
            await ServerRequest.SendRequest(request, CheckIfUserHasItemCallback);
        }

        [RequestCallbackMethod]
        private void CheckIfUserHasItemCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Neutral:
                    EventPublisher.TriggerCheckIfUserHasItem(returnRequest);
                    break;
                case RequestResultType.Fail:
                    EventPublisher.TriggerCheckIfUserHasItemErrorFired(returnRequest);
                    break;
                case RequestResultType.Success:
                    EventPublisher.TriggerCheckIfUserHasItem(returnRequest);
                    break;
                case RequestResultType.Undefined:
                    EventPublisher.TriggerCheckIfUserHasItemErrorFired(returnRequest);
                    break;
            }
        }
    }
}