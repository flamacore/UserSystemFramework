using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UserSystemFramework.Scripts.System.Config;
using UserSystemFramework.Scripts.System.Data.Classes;
using UserSystemFramework.Scripts.System.Services.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;
using UserSystemFramework.Scripts.System.Utilities;

namespace UserSystemFramework.Scripts.System.Data.Interfaces
{
    public interface IUser : IDatabaseEntry, IDataPusher
    {
        public string UserName { get; set; }
        public string UserToken { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int CoinBalance { get; set; }
        public int ClanId { get; set; }
        public string LocalTimeZone { get; set; }
        public double LocalTimeZoneOffset { get; set; }
        public string Language { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsGuestAccount { get; set; }
        public List<CustomFieldList> CustomUserData { get; set; }
        public Request UpdateRequest { get; }
        public ContactList<IUser> Contacts { get; set; }
        public List<ContactRequestData> SentRequests { get; set; }
        public List<ContactRequestData> ReceivedRequests { get; set; }
        public List<MessageData> LoadedMessages { get; set; }
        public List<ItemData> Items { get; set; }
        public List<Conversation> LoadedConversations { get; set; }
        public void CopyTokenFromRequest(IRequest requestToCopy);
        public Task<bool> UpdateUserData(Action<IRequest> updateUserCallback);
        
        public void DeserializeFromJson(string json)
        {
            JObject jsonDeserializeObject = JsonConvert.DeserializeObject<JObject>(json);
            if (jsonDeserializeObject != null)
            {
                jsonDeserializeObject.TryGetValue("id", out JToken idToken);
                jsonDeserializeObject.TryGetValue("userName", out JToken usernameToken);
                jsonDeserializeObject.TryGetValue("email", out JToken emailToken);
                jsonDeserializeObject.TryGetValue("password", out JToken passwordToken);
                jsonDeserializeObject.TryGetValue("firstName", out JToken firstNameToken);
                jsonDeserializeObject.TryGetValue("middleName", out JToken middleNameToken);
                jsonDeserializeObject.TryGetValue("lastName", out JToken lastNameToken);
                jsonDeserializeObject.TryGetValue("phone", out JToken phoneToken);
                jsonDeserializeObject.TryGetValue("coinBalance", out JToken coinBalanceToken);
                jsonDeserializeObject.TryGetValue("clanId", out JToken clanIdToken);
                jsonDeserializeObject.TryGetValue("localTimeZone", out JToken localTimeZoneToken);
                jsonDeserializeObject.TryGetValue("localTimeZoneOffset", out JToken localTimeZoneOffsetToken);
                jsonDeserializeObject.TryGetValue("language", out JToken languageToken);

                foreach (var keyValuePair in jsonDeserializeObject)
                {
                    ConfigService.CustomFieldsConfig.customUserFields.ForEach(x =>
                    {
                        if (keyValuePair.Key == x.fieldName)
                        {
                            SetCustomUserData(x.fieldName, keyValuePair.Value?.ToString());
                        }
                    });
                }

                ID = int.Parse(idToken.ToString());
                UserName = usernameToken.ToString();
                Email = emailToken.ToString();
                Password = passwordToken.ToString();
                FirstName = firstNameToken.ToString();
                MiddleName = middleNameToken.ToString();
                LastName = lastNameToken.ToString();
                Phone = phoneToken.ToString();
                CoinBalance = int.Parse(coinBalanceToken.ToString());
                ClanId = int.Parse(clanIdToken.ToString());
                LocalTimeZone = localTimeZoneToken.ToString();
                LocalTimeZoneOffset = double.Parse(localTimeZoneOffsetToken.ToString());
                Language = languageToken.ToString();
            }
        }

        public bool IsOnline => LastUpdated.AddSeconds(ConfigService.ServerConfig.heartbeatInterval)
            .CompareTo(DateTime.Now.ToUniversalTime()) != -1;

        public string GetCustomUserDataString(string fieldName)
            => CustomUserData.Find(x => x.fieldName == fieldName).FieldValueString;
        public int GetCustomUserDataInt(string fieldName) 
            => CustomUserData.Find(x => x.fieldName == fieldName).FieldValueString.ToInt();

        public void SetCustomUserData(string fieldName, string value, bool updateServerWithoutCallback = false)
        {
            CustomUserData.Find(x => x.fieldName == fieldName).Set(value);
            if (updateServerWithoutCallback)
                UpdateUserData(null);
        }
        
        public void SetCustomUserData(string fieldName, int value, bool updateServerWithoutCallback = false)
        {
            CustomUserData.Find(x => x.fieldName == fieldName).Set(value);
            if (updateServerWithoutCallback)
                UpdateUserData(null);
        }
        
        public void SetCustomUserData(string fieldName, DateTime value, bool updateServerWithoutCallback = false)
        {
            CustomUserData.Find(x => x.fieldName == fieldName).Set(value);
            if (updateServerWithoutCallback)
                UpdateUserData(null);
        }
        public void SetCustomUserData(string fieldName, bool value, bool updateServerWithoutCallback = false)
        {
            CustomUserData.Find(x => x.fieldName == fieldName).Set(value);
            if (updateServerWithoutCallback)
                UpdateUserData(null);
        }
    }
}