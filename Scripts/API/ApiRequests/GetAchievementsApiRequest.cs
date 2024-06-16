using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;

namespace UserSystemFramework.Scripts.API.ApiRequests
{
    public class GetAchievementsApiRequest : BaseApiRequest
    {
        public GetAchievementsApiRequest() { }
        public override void Call()
        {
            AchievementSystemController.Instance.GetAllAchievements();
            EventPublisher.OnGetAchievementsList += SuccessCallback;
            EventPublisher.OnGetAchievementsListErrorFired += FailureCallback;
        }

        public override void ReleaseUnmanagedResources()
        {
            base.ReleaseUnmanagedResources();
            EventPublisher.OnGetAchievementsList -= SuccessCallback;
            EventPublisher.OnGetAchievementsListErrorFired -= FailureCallback;
        }
    }
}