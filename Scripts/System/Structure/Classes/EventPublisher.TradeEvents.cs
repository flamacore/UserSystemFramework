using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Structure.Classes
{
    public static partial class EventPublisher
    {
        public delegate void AddItemToTrade(IRequest completedRequest);

        public static event AddItemToTrade OnAddItemToTrade;

        public static void TriggerAddItemToTrade(IRequest completedRequest) =>
            OnAddItemToTrade?.Invoke(completedRequest);

        public delegate void AddItemToTradeErrorFired(IRequest completedRequest);

        public static event AddItemToTradeErrorFired OnAddItemToTradeErrorFired;

        public static void TriggerAddItemToTradeErrorFired(IRequest completedRequest) =>
            OnAddItemToTradeErrorFired?.Invoke(completedRequest);

        public delegate void RemoveItemFromTrade(IRequest completedRequest);

        public static event RemoveItemFromTrade OnRemoveItemFromTrade;

        public static void TriggerRemoveItemFromTrade(IRequest completedRequest) =>
            OnRemoveItemFromTrade?.Invoke(completedRequest);

        public delegate void RemoveItemFromTradeErrorFired(IRequest completedRequest);

        public static event RemoveItemFromTradeErrorFired OnRemoveItemFromTradeErrorFired;

        public static void TriggerRemoveItemFromTradeErrorFired(IRequest completedRequest) =>
            OnRemoveItemFromTradeErrorFired?.Invoke(completedRequest);

        public delegate void GetTradeInventory(IRequest completedRequest);

        public static event GetTradeInventory OnGetTradeInventory;

        public static void TriggerGetTradeInventory(IRequest completedRequest) =>
            OnGetTradeInventory?.Invoke(completedRequest);

        public delegate void GetTradeInventoryErrorFired(IRequest completedRequest);

        public static event GetTradeInventoryErrorFired OnGetTradeInventoryErrorFired;

        public static void TriggerGetTradeInventoryErrorFired(IRequest completedRequest) =>
            OnGetTradeInventoryErrorFired?.Invoke(completedRequest);
    }
}