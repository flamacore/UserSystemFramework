using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UserSystemFramework.Scripts.System.Controllers.Attributes;
using UserSystemFramework.Scripts.System.Controllers.Interfaces;
using UserSystemFramework.Scripts.System.Data.Classes;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Services.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;
using UserSystemFramework.Scripts.System.Utilities;

namespace UserSystemFramework.Scripts.System.Controllers.Classes
{
    [DependsOn(Controller = typeof(LocalAccountController))]

    public class AchievementSystemController : BaseController<AchievementSystemController>, IController
    {
        protected ServerRequestSenderService ServerRequest { private set; get; }
        public static List<AchievementData> Achievements = new List<AchievementData>();
        
        [ControllerInitialization]
        public override IEnumerator InitializeController()
        {
            yield return base.InitializeController();
            yield return new WaitForControllers<AchievementSystemController>();
            ServerRequest = ServiceHandler.Locator.Get<ServerRequestSenderService>();
            CompleteInitialization();
        }

        internal async void GetAllAchievements()
        {
            IRequest getAchievementsListRequest = ServerRequestGetterService.Get(RequestType.GetAchievementsListRequest);
            await ServerRequest.SendRequest(getAchievementsListRequest, GetAllAchievementsCallback);
        }

        [RequestCallbackMethod]
        private void GetAllAchievementsCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Success:
                    Achievements =
                        JsonConvert.DeserializeObject<List<AchievementData>>(
                            returnRequest.ConnectionResponseHeaders["RequestResult"]);
                    EventPublisher.TriggerGetAchievementsList(returnRequest);
                    break;
                case RequestResultType.Neutral:
                    EventPublisher.TriggerGetAchievementsList(returnRequest);
                    break;
                case RequestResultType.Fail:
                case RequestResultType.Undefined:
                    EventPublisher.TriggerGetAchievementsListErrorFired(returnRequest);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal async void ChangeAchievementStatus(int achievementId, int toStatus, int progress, int forUserId = -99)
        {
            IRequest getAchievementsListRequest = ServerRequestGetterService.Get(RequestType.ChangeAchievementStatusForUserRequest, false, false, new Dictionary<string, string>()
            {
                {"forUserId", forUserId == -99 ? LocalAccountController.CurrentLocalUser.ID.ToString() : forUserId.ToString()},
                {"toStatus", toStatus.ToString()},
                {"progress", progress.ToString()},
                {"achievementId", achievementId.ToString()}
            });
            await ServerRequest.SendRequest(getAchievementsListRequest, ChangeAchievementStatusCallback);
        }
        
        [RequestCallbackMethod]
        private void ChangeAchievementStatusCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Success:
                case RequestResultType.Neutral:
                    EventPublisher.TriggerChangeAchievementStatusForUser(returnRequest);
                    break;
                case RequestResultType.Fail:
                case RequestResultType.Undefined:
                    EventPublisher.TriggerChangeAchievementStatusForUserErrorFired(returnRequest);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal async void CheckAchievementForUser(int achievementId, int forUserId = -99)
        {
            IRequest getAchievementsListRequest = ServerRequestGetterService.Get(RequestType.CheckAchievementStatusForUserRequest, true, false, new Dictionary<string, string>()
            {
                {"forUserId", forUserId == -99 ? LocalAccountController.CurrentLocalUser.ID.ToString() : forUserId.ToString()},
                {"achievementId", achievementId.ToString()}
            });
            await ServerRequest.SendRequest(getAchievementsListRequest, CheckAchievementForUserCallback);
        }
        
        [RequestCallbackMethod]
        private void CheckAchievementForUserCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Success:
                case RequestResultType.Neutral:
                    // ReSharper disable once PossibleNullReferenceException
                    UserAchievementData data = JsonConvert.DeserializeObject<List<UserAchievementData>>(
                        returnRequest.ConnectionResponseHeaders["RequestResult"])[0];
                    EventPublisher.TriggerCheckAchievementStatusForUser(returnRequest, data);
                    break;
                case RequestResultType.Fail:
                case RequestResultType.Undefined:
                    EventPublisher.TriggerChangeAchievementStatusForUserErrorFired(returnRequest);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        internal async void AddAchievement(string achievementName, string description, string shortDescription, int requiredPoints)
        {
            IRequest getAchievementsListRequest = ServerRequestGetterService.Get(RequestType.AddAchievementRequest, true, false, new Dictionary<string, string>()
            {
                {"achievementName", achievementName},
                {"description", description},
                {"shortDescription", description},
                {"requiredPoints", description}
            });
            await ServerRequest.SendRequest(getAchievementsListRequest, AddAchievementCallback);
        }
        
        [RequestCallbackMethod]
        private void AddAchievementCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Success:
                case RequestResultType.Neutral:
                    EventPublisher.TriggerAddAchievement(returnRequest);
                    break;
                case RequestResultType.Fail:
                case RequestResultType.Undefined:
                    EventPublisher.TriggerAddAchievementErrorFired(returnRequest);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        internal async void RemoveAchievement(int achievementId)
        {
            IRequest getAchievementsListRequest = ServerRequestGetterService.Get(RequestType.RemoveAchievementRequest, true, false, new Dictionary<string, string>()
            {
                {"achievementId", achievementId.ToString()}
            });
            await ServerRequest.SendRequest(getAchievementsListRequest, RemoveAchievementCallback);
        }
        internal async void RemoveAchievement(string achievementName)
        {
            IRequest getAchievementsListRequest = ServerRequestGetterService.Get(RequestType.RemoveAchievementRequest, true, false, new Dictionary<string, string>()
            {
                {"achievementName", achievementName}
            });
            await ServerRequest.SendRequest(getAchievementsListRequest, RemoveAchievementCallback);
        }
        
        [RequestCallbackMethod]
        private void RemoveAchievementCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Success:
                case RequestResultType.Neutral:
                    EventPublisher.TriggerRemoveAchievement(returnRequest);
                    break;
                case RequestResultType.Fail:
                case RequestResultType.Undefined:
                    EventPublisher.TriggerRemoveAchievementErrorFired(returnRequest);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        //TODO: Move to editor
        internal async void UpdateAchievement(int achievementId, string achievementName, string description, string shortDescription, int requiredPoints)
        {
            IRequest getAchievementsListRequest = ServerRequestGetterService.Get(RequestType.UpdateAchievementRequest, true, false, new Dictionary<string, string>()
            {
                {"achievementId", achievementId.ToString()},
                {"achievementName", achievementName},
                {"description", description},
                {"shortDescription", description},
                {"requiredPoints", description}
            });
            await ServerRequest.SendRequest(getAchievementsListRequest, UpdateAchievementCallback);
        }

        [RequestCallbackMethod]
        private void UpdateAchievementCallback(IRequest returnRequest)
        {
            switch (returnRequest.ResultType)
            {
                case RequestResultType.Success:
                case RequestResultType.Neutral:
                    EventPublisher.TriggerUpdateAchievement(returnRequest);
                    break;
                case RequestResultType.Fail:
                case RequestResultType.Undefined:
                    EventPublisher.TriggerUpdateAchievementErrorFired(returnRequest);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}