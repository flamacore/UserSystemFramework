//TODO: Move to Editor folder
using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class RemoveAchievementApiRequest : BaseApiRequest
    {
        private readonly string _achievementName = null;
        private readonly int _achievementId = -1;

        public RemoveAchievementApiRequest(int achievementId)
        {
            _achievementId = achievementId;
        }
        public RemoveAchievementApiRequest(string achievementName)
        {
            _achievementName = achievementName;
        }

        public override void Call()
        {
            if (_achievementId == -1)
            {
                AchievementSystemController.Instance.RemoveAchievement(_achievementName);
            }
            else
            {
                AchievementSystemController.Instance.RemoveAchievement(_achievementId);
            }
            EventPublisher.OnRemoveAchievement += SuccessCallback;
            EventPublisher.OnRemoveAchievementErrorFired += FailureCallback;
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnRemoveAchievement -= SuccessCallback;
            EventPublisher.OnRemoveAchievementErrorFired -= FailureCallback;
        }
    }
}