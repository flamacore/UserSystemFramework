//TODO: Move to Editor folder
using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class AddAchievementApiRequest : BaseApiRequest
    {
        private readonly string _achievementName;
        private readonly string _description;
        private readonly string _shortDescription;
        private readonly int _requiredPoints;

        public AddAchievementApiRequest(string achievementName, string description, string shortDescription, int requiredPoints)
        {
            _achievementName = achievementName;
            _description = description;
            _shortDescription = shortDescription;
            _requiredPoints = requiredPoints;
        }

        public override void Call()
        {
            AchievementSystemController.Instance.AddAchievement(_achievementName, _description, _shortDescription, _requiredPoints);
            EventPublisher.OnAddAchievement += SuccessCallback;
            EventPublisher.OnAddAchievementErrorFired += FailureCallback;
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnAddAchievement -= SuccessCallback;
            EventPublisher.OnAddAchievementErrorFired -= FailureCallback;
        }
    }
}