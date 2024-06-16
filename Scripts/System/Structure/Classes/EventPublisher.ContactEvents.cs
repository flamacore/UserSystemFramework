using System;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Structure.Classes
{
    public static partial class EventPublisher
    {
        public delegate void GetContactsComplete(IRequest completedRequest);
        public static event GetContactsComplete OnGetContactsComplete;
        public static void TriggerGetContactsComplete(IRequest completedRequest) =>
            OnGetContactsComplete?.Invoke(completedRequest);
        public delegate void GetContactsErrorFired(IRequest completedRequest);
        public static event GetContactsErrorFired OnGetContactsErrorFired;
        public static void TriggerGetContactsErrorFired(IRequest completedRequest) =>
            OnGetContactsErrorFired?.Invoke(completedRequest);
        public delegate void ChangeContactRelationshipComplete(IRequest completedRequest);
        public static event ChangeContactRelationshipComplete OnChangeContactRelationshipComplete;
        public static void TriggerChangeContactRelationshipComplete(IRequest completedRequest) =>
            OnChangeContactRelationshipComplete?.Invoke(completedRequest);
        public delegate void ChangeContactRelationshipErrorFired(IRequest completedRequest);
        public static event ChangeContactRelationshipErrorFired OnChangeContactRelationshipErrorFired;
        public static void TriggerChangeContactRelationshipErrorFired(IRequest completedRequest) =>
            OnChangeContactRelationshipErrorFired?.Invoke(completedRequest);
        public delegate void GetMySentRequests(IRequest completedRequest);
        [Obsolete("This event is obsolete. Please use OnGetContacts instead")]
        public static event GetMySentRequests OnGetMySentRequests;
        public static void TriggerGetMySentRequests(IRequest completedRequest) =>
            OnGetMySentRequests?.Invoke(completedRequest);
        public delegate void GetMySentRequestsErrorFired(IRequest completedRequest);
        [Obsolete("This event is obsolete. Please use OnGetContacts instead")]
        public static event GetMySentRequestsErrorFired OnGetMySentRequestsErrorFired;
        public static void TriggerGetMySentRequestsErrorFired(IRequest completedRequest) =>
            OnGetMySentRequestsErrorFired?.Invoke(completedRequest);
        public delegate void GetMyRequests(IRequest completedRequest);
        [Obsolete("This event is obsolete. Please use OnGetContacts instead")]
        public static event GetMyRequests OnGetMyRequests;
        public static void TriggerGetMyRequests(IRequest completedRequest) => OnGetMyRequests?.Invoke(completedRequest);
        public delegate void GetMyRequestsErrorFired(IRequest completedRequest);
        [Obsolete("This event is obsolete. Please use OnGetContacts instead")]
        public static event GetMyRequestsErrorFired OnGetMyRequestsErrorFired;
        public static void TriggerGetMyRequestsErrorFired(IRequest completedRequest) => OnGetMyRequestsErrorFired?.Invoke(completedRequest);
    }
}