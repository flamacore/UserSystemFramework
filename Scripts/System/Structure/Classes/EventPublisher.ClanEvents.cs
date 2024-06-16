using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Structure.Classes
{
    public static partial class EventPublisher
    {
        public delegate void AddUserToClan(IRequest completedRequest);
        public static event AddUserToClan OnAddUserToClan;
        public static void TriggerAddUserToClan(IRequest completedRequest) => OnAddUserToClan?.Invoke(completedRequest);
        public delegate void AddUserToClanErrorFired(IRequest completedRequest);
        public static event AddUserToClanErrorFired OnAddUserToClanErrorFired;
        public static void TriggerAddUserToClanErrorFired(IRequest completedRequest) =>
            OnAddUserToClanErrorFired?.Invoke(completedRequest);
        public delegate void RemoveUserFromClan(IRequest completedRequest);
        public static event RemoveUserFromClan OnRemoveUserFromClan;
        public static void TriggerRemoveUserFromClan(IRequest completedRequest) =>
            OnRemoveUserFromClan?.Invoke(completedRequest);
        public delegate void RemoveUserFromClanErrorFired(IRequest completedRequest);
        public static event RemoveUserFromClanErrorFired OnRemoveUserFromClanErrorFired;
        public static void TriggerRemoveUserFromClanErrorFired(IRequest completedRequest) =>
            OnRemoveUserFromClanErrorFired?.Invoke(completedRequest);
        public delegate void ChangeUserRank(IRequest completedRequest);
        public static event ChangeUserRank OnChangeUserRank;
        public static void TriggerChangeUserRank(IRequest completedRequest) =>
            OnChangeUserRank?.Invoke(completedRequest);
        public delegate void ChangeUserRankErrorFired(IRequest completedRequest);
        public static event ChangeUserRankErrorFired OnChangeUserRankErrorFired;
        public static void TriggerChangeUserRankErrorFired(IRequest completedRequest) =>
            OnChangeUserRankErrorFired?.Invoke(completedRequest);
        public delegate void BroadcastMessage(IRequest completedRequest);
        public static event BroadcastMessage OnBroadcastMessage;
        public static void TriggerBroadcastMessage(IRequest completedRequest) =>
            OnBroadcastMessage?.Invoke(completedRequest);
        public delegate void BroadcastMessageErrorFired(IRequest completedRequest);
        public static event BroadcastMessageErrorFired OnBroadcastMessageErrorFired;
        public static void TriggerBroadcastMessageErrorFired(IRequest completedRequest) =>
            OnBroadcastMessageErrorFired?.Invoke(completedRequest);
        public delegate void SendClanMessage(IRequest completedRequest);
        public static event SendClanMessage OnSendClanMessage;
        public static void TriggerSendClanMessage(IRequest completedRequest) =>
            OnSendClanMessage?.Invoke(completedRequest);
        public delegate void SendClanMessageErrorFired(IRequest completedRequest);
        public static event SendClanMessageErrorFired OnSendClanMessageErrorFired;
        public static void TriggerSendClanMessageErrorFired(IRequest completedRequest) =>
            OnSendClanMessageErrorFired?.Invoke(completedRequest);
        public delegate void DeleteClanMessage(IRequest completedRequest);
        public static event DeleteClanMessage OnDeleteClanMessage;
        public static void TriggerDeleteClanMessage(IRequest completedRequest) =>
            OnDeleteClanMessage?.Invoke(completedRequest);
        public delegate void DeleteClanMessageErrorFired(IRequest completedRequest);
        public static event DeleteClanMessageErrorFired OnDeleteClanMessageErrorFired;
        public static void TriggerDeleteClanMessageErrorFired(IRequest completedRequest) =>
            OnDeleteClanMessageErrorFired?.Invoke(completedRequest);
        public delegate void GetClanMessages(IRequest completedRequest);
        public static event GetClanMessages OnGetClanMessages;
        public static void TriggerGetClanMessages(IRequest completedRequest) =>
            OnGetClanMessages?.Invoke(completedRequest);
        public delegate void GetClanMessagesErrorFired(IRequest completedRequest);
        public static event GetClanMessagesErrorFired OnGetClanMessagesErrorFired;
        public static void TriggerGetClanMessagesErrorFired(IRequest completedRequest) =>
            OnGetClanMessagesErrorFired?.Invoke(completedRequest);
        public delegate void CreateClan(IRequest completedRequest);
        public static event CreateClan OnCreateClan;
        public static void TriggerCreateClan(IRequest completedRequest) => OnCreateClan?.Invoke(completedRequest);
        public delegate void CreateClanErrorFired(IRequest completedRequest);
        public static event CreateClanErrorFired OnCreateClanErrorFired;
        public static void TriggerCreateClanErrorFired(IRequest completedRequest) =>
            OnCreateClanErrorFired?.Invoke(completedRequest);
        public delegate void DeleteClan(IRequest completedRequest);
        public static event DeleteClan OnDeleteClan;
        public static void TriggerDeleteClan(IRequest completedRequest) => OnDeleteClan?.Invoke(completedRequest);
        public delegate void DeleteClanErrorFired(IRequest completedRequest);
        public static event DeleteClanErrorFired OnDeleteClanErrorFired;
        public static void TriggerDeleteClanErrorFired(IRequest completedRequest) =>
            OnDeleteClanErrorFired?.Invoke(completedRequest);
        public delegate void GetClans(IRequest completedRequest);
        public static event GetClans OnGetClans;
        public static void TriggerGetClans(IRequest completedRequest) => OnGetClans?.Invoke(completedRequest);
        public delegate void GetClansErrorFired(IRequest completedRequest);
        public static event GetClansErrorFired OnGetClansErrorFired;
        public static void TriggerGetClansErrorFired(IRequest completedRequest) =>
            OnGetClansErrorFired?.Invoke(completedRequest);
        public delegate void EditClan(IRequest completedRequest);
        public static event EditClan OnEditClan;
        public static void TriggerEditClan(IRequest completedRequest) => OnEditClan?.Invoke(completedRequest);
        public delegate void EditClanErrorFired(IRequest completedRequest);
        public static event EditClanErrorFired OnEditClanErrorFired;
        public static void TriggerEditClanErrorFired(IRequest completedRequest) =>
            OnEditClanErrorFired?.Invoke(completedRequest);
    }
}