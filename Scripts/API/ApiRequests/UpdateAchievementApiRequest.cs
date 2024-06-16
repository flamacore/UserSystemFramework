//TODO: Move to Editor folder
using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class UpdateAchievementApiRequest : BaseApiRequest
    {
        private readonly int _achievementId;
        private readonly string _achievementName;
        private readonly string _achievementDescription;
        private readonly string _achievementShortDescription;
        private readonly int _requiredPoints;
        
        public UpdateAchievementApiRequest(int achievementId, string achievementName, string achievementDescription, string achievementShortDescription, int requiredPoints)
        {
            _achievementId = achievementId;
            _achievementName = achievementName;
            _achievementDescription = achievementDescription;
            _achievementShortDescription = achievementShortDescription;
            _requiredPoints = requiredPoints;
        }

        public override void Call()
        {
            AchievementSystemController.Instance.UpdateAchievement(_achievementId, _achievementName, _achievementDescription, _achievementShortDescription, _requiredPoints);
            EventPublisher.OnUpdateAchievement += SuccessCallback;
            EventPublisher.OnUpdateAchievementErrorFired += FailureCallback;
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnUpdateAchievement -= SuccessCallback;
            EventPublisher.OnUpdateAchievementErrorFired -= FailureCallback;
        }
        
    }
}