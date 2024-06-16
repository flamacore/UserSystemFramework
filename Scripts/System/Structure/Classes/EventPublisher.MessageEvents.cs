using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Structure.Classes
{
    public static partial class EventPublisher
    {
        public delegate void SendMessageComplete(IRequest completedRequest);
        public static event SendMessageComplete OnSendMessageComplete;
        public static void TriggerSendMessageComplete(IRequest completedRequest) =>
            OnSendMessageComplete?.Invoke(completedRequest);
        public delegate void SendMessageCompleteErrorFired(IRequest completedRequest);
        public static event SendMessageCompleteErrorFired OnSendMessageCompleteErrorFired;
        public static void TriggerSendMessageCompleteErrorFired(IRequest completedRequest) =>
            OnSendMessageCompleteErrorFired?.Invoke(completedRequest);
        public delegate void GetMessages(IRequest completedRequest);
        public static event GetMessages OnGetMessages;
        public static void TriggerGetMessages(IRequest completedRequest) => OnGetMessages?.Invoke(completedRequest);
        public delegate void GetMessagesErrorFired(IRequest completedRequest);
        public static event GetMessagesErrorFired OnGetMessagesErrorFired;
        public static void TriggerGetMessagesErrorFired(IRequest completedRequest) =>
            OnGetMessagesErrorFired?.Invoke(completedRequest);
        public delegate void GetMessagesWithUser(IRequest completedRequest);
        public static event GetMessagesWithUser OnGetMessagesWithUser;
        public static void TriggerGetMessagesWithUser(IRequest completedRequest) =>
            OnGetMessagesWithUser?.Invoke(completedRequest);
        public delegate void GetMessagesWithUserErrorFired(IRequest completedRequest);
        public static event GetMessagesWithUserErrorFired OnGetMessagesWithUserErrorFired;
        public static void TriggerGetMessagesWithUserErrorFired(IRequest completedRequest) =>
            OnGetMessagesWithUserErrorFired?.Invoke(completedRequest);
        public delegate void ReadMessage(IRequest completedRequest);
        public static event ReadMessage OnReadMessage;
        public static void TriggerReadMessage(IRequest completedRequest) => OnReadMessage?.Invoke(completedRequest);
        public delegate void ReadMessageErrorFired(IRequest completedRequest);
        public static event ReadMessageErrorFired OnReadMessageErrorFired;
        public static void TriggerReadMessageErrorFired(IRequest completedRequest) =>
            OnReadMessageErrorFired?.Invoke(completedRequest);
        public delegate void DeleteMessage(IRequest completedRequest);
        public static event DeleteMessage OnDeleteMessage;
        public static void TriggerDeleteMessage(IRequest completedRequest) => OnDeleteMessage?.Invoke(completedRequest);
        public delegate void DeleteMessageErrorFired(IRequest completedRequest);
        public static event DeleteMessageErrorFired OnDeleteMessageErrorFired;
        public static void TriggerDeleteMessageErrorFired(IRequest completedRequest) =>
            OnDeleteMessageErrorFired?.Invoke(completedRequest);
    }
}