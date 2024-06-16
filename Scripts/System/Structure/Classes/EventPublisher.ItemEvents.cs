using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Structure.Classes
{
    public static partial class EventPublisher
    {
        public delegate void AddItemToUser(IRequest completedRequest);
        public static event AddItemToUser OnAddItemToUser;
        public static void TriggerAddItemToUser(IRequest completedRequest) => OnAddItemToUser?.Invoke(completedRequest);
        public delegate void AddItemToUserErrorFired(IRequest completedRequest);
        public static event AddItemToUserErrorFired OnAddItemToUserErrorFired;
        public static void TriggerAddItemToUserErrorFired(IRequest completedRequest) =>
            OnAddItemToUserErrorFired?.Invoke(completedRequest);
        public delegate void RemoveItemFromUser(IRequest completedRequest);
        public static event RemoveItemFromUser OnRemoveItemFromUser;
        public static void TriggerRemoveItemFromUser(IRequest completedRequest) =>
            OnRemoveItemFromUser?.Invoke(completedRequest);
        public delegate void RemoveItemFromUserErrorFired(IRequest completedRequest);
        public static event RemoveItemFromUserErrorFired OnRemoveItemFromUserErrorFired;
        public static void TriggerRemoveItemFromUserErrorFired(IRequest completedRequest) =>
            OnRemoveItemFromUserErrorFired?.Invoke(completedRequest);
        public delegate void GetUserInventory(IRequest completedRequest);
        public static event GetUserInventory OnGetUserInventory;
        public static void TriggerGetUserInventory(IRequest completedRequest) =>
            OnGetUserInventory?.Invoke(completedRequest);
        public delegate void GetUserInventoryErrorFired(IRequest completedRequest);
        public static event GetUserInventoryErrorFired OnGetUserInventoryErrorFired;
        public static void TriggerGetUserInventoryErrorFired(IRequest completedRequest) =>
            OnGetUserInventoryErrorFired?.Invoke(completedRequest);
        public delegate void CheckIfUserHasItem(IRequest completedRequest);
        public static event CheckIfUserHasItem OnCheckIfUserHasItem;
        public static void TriggerCheckIfUserHasItem(IRequest completedRequest) =>
            OnCheckIfUserHasItem?.Invoke(completedRequest);
        public delegate void CheckIfUserHasItemErrorFired(IRequest completedRequest);
        public static event CheckIfUserHasItemErrorFired OnCheckIfUserHasItemErrorFired;
        public static void TriggerCheckIfUserHasItemErrorFired(IRequest completedRequest) =>
            OnCheckIfUserHasItemErrorFired?.Invoke(completedRequest);
        public delegate void GetItems(IRequest completedRequest);
        public static event GetItems OnGetItems;
        public static void TriggerGetItems(IRequest completedRequest) => OnGetItems?.Invoke(completedRequest);
        public delegate void GetItemsErrorFired(IRequest completedRequest);
        public static event GetItemsErrorFired OnGetItemsErrorFired;
        public static void TriggerGetItemsErrorFired(IRequest completedRequest) =>
            OnGetItemsErrorFired?.Invoke(completedRequest);
    }
}