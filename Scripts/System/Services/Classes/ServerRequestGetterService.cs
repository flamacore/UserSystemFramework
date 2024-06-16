using System.Collections.Generic;
using UserSystemFramework.Scripts.System.Config;
using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;
using UserSystemFramework.Scripts.System.Utilities;

namespace UserSystemFramework.Scripts.System.Services.Classes
{
    public enum RequestType
    {
        SendMessageRequest,
        GetMessagesRequest,
        GetMessagesWithUserRequest,
        ReadMessageRequest,
        DeleteMessageRequest,
        
        AddUserToClanRequest,
        RemoveUserFromClanRequest,
        ChangeUserRankRequest,
        BroadcastMessageRequest,
        SendClanMessageRequest,
        DeleteClanMessageRequest,
        GetClanMessagesRequest,
        CreateClanRequest,
        DeleteClanRequest,
        GetClansRequest,
        EditClanRequest,
        
        AddItemToUserRequest,
        RemoveItemFromUserRequest,
        GetUserInventoryRequest,
        CheckIfUserHasItemRequest,
        GetItemsRequest,
        
        AddItemToTradeRequest,
        RemoveItemFromTradeRequest,
        GetTradeInventoryRequest,
        
        ChangeAchievementStatusForUserRequest,
        CheckAchievementStatusForUserRequest,
        GetAchievementsListRequest,
        
        ContactGetterRequest,
        MySentRequestsGetterRequest,
        MyRequestsGetterRequest,
        ContactStatusChangerRequest,
        
        HeartbeatRequest,
        RegisterRequest,
        LoginRequest,
        DeleteAccountRequest,
        UpdateUserRequest,
        InitialRequest,
        
        GetTimeRequest,

        //TODO: separate editor requests
        AddAchievementRequest,
        UpdateAchievementRequest,
        RemoveAchievementRequest,
        
        AddItemRequest,
        RemoveItemRequest,
        UpdateItemRequest
    }
    public class ServerRequestGetterService : Service
    {
        public override ServicePriority Priority => ServicePriority.Medium;
        /// <summary>
        /// This is where all the conventions between Unity and PHP Server must match.
        /// Please be very careful if you aim to add more requests here and check how it's handled
        /// on the server side. Very easy to miss things here.
        /// </summary>
        /// <param name="request">Type of the request. On PHP side, the name must exactly match.</param>
        /// <param name="includeUserData"></param>
        /// <param name="includeToken"></param>
        /// <param name="additionalParameters"></param>
        /// <param name="endPoint"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IRequest Get(RequestType request, 
             bool includeUserData = true, 
             bool includeToken = false, 
             Dictionary<string, string> additionalParameters = null,  
             string endPoint = "USF.Wrapper.php", 
             ServerConfiguration config = null)
        {
            Dictionary<string, string> paramSet = new Dictionary<string, string>();
            paramSet.Add("route", request.ToString());
            if(includeToken) paramSet.Add("token", "");
            if(includeUserData) paramSet.Add("userData", JsonConvertExtension.SerializeObject(LocalAccountController.CurrentLocalUser));
            if (additionalParameters != null)
            {
                foreach (KeyValuePair<string,string> additionalParameter in additionalParameters)
                {
                    paramSet.Add(additionalParameter.Key, additionalParameter.Value);
                }
            }
            return new Request(endPoint, config ? config : ConfigService.ServerConfig, paramSet, request);
        }
    }
}