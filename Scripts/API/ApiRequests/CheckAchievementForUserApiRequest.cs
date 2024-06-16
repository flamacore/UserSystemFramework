using UserSystemFramework.Scripts.API.Interfaces;
using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Classes;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class CheckAchievementForUserApiRequest : BaseApiRequest
    {
        private readonly IUser _userData;
        private readonly int _achievementId;
        public delegate void SuccessCallbackEventWithDataEvent(IRequest completedRequest, UserAchievementData achievementData);
        public event SuccessCallbackEventWithDataEvent SuccessCallbackEventWithData;

        public CheckAchievementForUserApiRequest(IUser userData, int achievementId)
        {
            _userData = userData;
            _achievementId = achievementId;
        }

        public override void Call()
        {
            AchievementSystemController.Instance.CheckAchievementForUser(_achievementId, _userData.ID);
            EventPublisher.OnCheckAchievementStatusForUser += CustomSuccessCallback;
            EventPublisher.OnCheckAchievementStatusForUserErrorFired += FailureCallback;
        }

        private void CustomSuccessCallback(IRequest request, UserAchievementData achievementData)
        {
            SuccessCallbackEventWithData?.Invoke(request, achievementData);
            SuccessCallback(request);
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnCheckAchievementStatusForUser -= CustomSuccessCallback;
            EventPublisher.OnCheckAchievementStatusForUserErrorFired -= FailureCallback;
        }
    }
}