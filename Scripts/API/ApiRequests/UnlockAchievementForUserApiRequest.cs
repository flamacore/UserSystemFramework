using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class UnlockAchievementForUserApiRequest : BaseApiRequest
    {
        private IUser _userData;
        private int _achievementId;
        private int _progress;
        public UnlockAchievementForUserApiRequest(IUser userData, int achievementId, int progress)
        {
            _userData = userData;
            _achievementId = achievementId;
            _progress = progress;
        }

        public override void Call()
        {
            AchievementSystemController.Instance.ChangeAchievementStatus(_achievementId, 1, _progress, _userData.ID);
            EventPublisher.OnChangeAchievementStatusForUser += SuccessCallback;
            EventPublisher.OnChangeAchievementStatusForUserErrorFired += FailureCallback;
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnChangeAchievementStatusForUser -= SuccessCallback;
            EventPublisher.OnChangeAchievementStatusForUserErrorFired -= FailureCallback;
        }
    }
}