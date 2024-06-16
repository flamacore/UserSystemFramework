using UserSystemFramework.Scripts.System.Data.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Structure.Classes
{
    public static partial class EventPublisher
    {
        public delegate void ChangeAchievementStatusForUser(IRequest completedRequest);
        public static event ChangeAchievementStatusForUser OnChangeAchievementStatusForUser;
        public static void TriggerChangeAchievementStatusForUser(IRequest completedRequest) =>
            OnChangeAchievementStatusForUser?.Invoke(completedRequest);
        public delegate void ChangeAchievementStatusForUserErrorFired(IRequest completedRequest);
        public static event ChangeAchievementStatusForUserErrorFired OnChangeAchievementStatusForUserErrorFired;
        public static void TriggerChangeAchievementStatusForUserErrorFired(IRequest completedRequest) =>
            OnChangeAchievementStatusForUserErrorFired?.Invoke(completedRequest);
        public delegate void CheckAchievementStatusForUser(IRequest completedRequest,
            UserAchievementData achievementData);
        public static event CheckAchievementStatusForUser OnCheckAchievementStatusForUser;
        public static void TriggerCheckAchievementStatusForUser(IRequest completedRequest,
            UserAchievementData achievementData) =>
            OnCheckAchievementStatusForUser?.Invoke(completedRequest, achievementData);
        public delegate void CheckAchievementStatusForUserErrorFired(IRequest completedRequest);
        public static event CheckAchievementStatusForUserErrorFired OnCheckAchievementStatusForUserErrorFired;
        public static void TriggerCheckAchievementStatusForUserErrorFired(IRequest completedRequest) =>
            OnCheckAchievementStatusForUserErrorFired?.Invoke(completedRequest);
        public delegate void GetAchievementsList(IRequest completedRequest);
        public static event GetAchievementsList OnGetAchievementsList;
        public static void TriggerGetAchievementsList(IRequest completedRequest) =>
            OnGetAchievementsList?.Invoke(completedRequest);
        public delegate void GetAchievementsListErrorFired(IRequest completedRequest);
        public static event GetAchievementsListErrorFired OnGetAchievementsListErrorFired;
        public static void TriggerGetAchievementsListErrorFired(IRequest completedRequest) =>
            OnGetAchievementsListErrorFired?.Invoke(completedRequest);
        public delegate void AddAchievement(IRequest completedRequest);
        public static event AddAchievement OnAddAchievement;
        public static void TriggerAddAchievement(IRequest completedRequest) =>
            OnAddAchievement?.Invoke(completedRequest);
        public delegate void AddAchievementErrorFired(IRequest completedRequest);
        public static event AddAchievementErrorFired OnAddAchievementErrorFired;
        public static void TriggerAddAchievementErrorFired(IRequest completedRequest) =>
            OnAddAchievementErrorFired?.Invoke(completedRequest);
        public delegate void RemoveAchievement(IRequest completedRequest);
        public static event RemoveAchievement OnRemoveAchievement;
        public static void TriggerRemoveAchievement(IRequest completedRequest) =>
            OnRemoveAchievement?.Invoke(completedRequest);
        public delegate void RemoveAchievementErrorFired(IRequest completedRequest);
        public static event RemoveAchievementErrorFired OnRemoveAchievementErrorFired;
        public static void TriggerRemoveAchievementErrorFired(IRequest completedRequest) =>
            OnRemoveAchievementErrorFired?.Invoke(completedRequest);
        public delegate void UpdateAchievement(IRequest completedRequest);
        public static event UpdateAchievement OnUpdateAchievement;
        public static void TriggerUpdateAchievement(IRequest completedRequest) => OnUpdateAchievement?.Invoke(completedRequest);
        public delegate void UpdateAchievementErrorFired(IRequest completedRequest);
        public static event UpdateAchievementErrorFired OnUpdateAchievementErrorFired;
        public static void TriggerUpdateAchievementErrorFired(IRequest completedRequest) => OnUpdateAchievementErrorFired?.Invoke(completedRequest);
        
    }
}