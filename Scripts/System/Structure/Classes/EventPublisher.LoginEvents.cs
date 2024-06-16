using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Structure.Classes
{
    public static partial class EventPublisher
    {
        public delegate void GuestAccountRegistered(IRequest completedRequest);
        public static event GuestAccountRegistered OnGuestAccountRegistered;
        public static void TriggerGuestAccountRegistered(IRequest completedRequest) =>
            OnGuestAccountRegistered?.Invoke(completedRequest);
        public delegate void RealAccountRegistered(IRequest completedRequest);
        public static event RealAccountRegistered OnRealAccountRegistered;
        public static void TriggerRealAccountRegistered(IRequest completedRequest) =>
            OnRealAccountRegistered?.Invoke(completedRequest);
        public delegate void LoginComplete(IRequest completedRequest);
        public static event LoginComplete OnLoginComplete;
        public static void TriggerLoginComplete(IRequest completedRequest) => OnLoginComplete?.Invoke(completedRequest);
        public delegate void LoginErrorFired(IRequest completedRequest);
        public static event LoginErrorFired OnLoginErrorFired;
        public static void TriggerLoginErrorFired(IRequest completedRequest) =>
            OnLoginErrorFired?.Invoke(completedRequest);
        public delegate void LogoutComplete(IRequest completedRequest);
        public static event LogoutComplete OnLogoutComplete;
        public static void TriggerLogoutComplete(IRequest completedRequest) =>
            OnLogoutComplete?.Invoke(completedRequest);
        public delegate void LogoutErrorFired(IRequest completedRequest);
        public static event LogoutErrorFired OnLogoutErrorFired;
        public static void TriggerLogoutErrorFired(IRequest completedRequest) =>
            OnLogoutErrorFired?.Invoke(completedRequest);
    }
}