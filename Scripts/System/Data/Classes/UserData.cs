using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserSystemFramework.Scripts.System.Config;
using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Interfaces;
using UserSystemFramework.Scripts.System.Services.Classes;
using UserSystemFramework.Scripts.System.Structure.Classes;
using UserSystemFramework.Scripts.System.Structure.Interfaces;

namespace UserSystemFramework.Scripts.System.Data.Classes
{
    /// <summary>
    ///  Class that holds all the data for a user account and implements the IUser interface.
    ///  This class is used to store the data for a user account and is used to create a new user account.
    /// </summary>
    [Serializable]
    public class UserData : IUser
    {
        private int _id;
        private string _userName;
        private string _email;
        private string _password;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _phone;
        private int _coinBalance;
        private int _clanId;
        private string _localTimeZone;
        private double _localTimeZoneOffset;
        private string _language;
        
        /// <summary>
        ///  The user token is used to identify the user when making API calls.
        /// </summary>
        private string _userToken;
        private DateTime _created;
        private DateTime _lastUpdated;
        private int _status;
        
        /// <summary>
        ///  The custom user data is used to store custom data for the user.
        ///  This data is stored in a list of CustomFieldList objects.
        ///  Each CustomFieldList object contains a list of CustomField objects.
        ///  Each CustomField object contains a key and a value.
        ///  The key is used to identify the data and the value is the actual data.
        ///  The key is a string and the value is an object.
        ///  The value can be any type of object.
        /// </summary>
        private List<CustomFieldList> _customUserData;
        
        /// <summary>
        ///  The isLoggedIn variable is used to store the login status of the user.
        /// </summary>
        private bool _isLoggedIn;
        
        /// <summary>
        ///  The isGuestAccount variable is used to store the guest account status of the user.
        ///  The user will only be able to make API calls if the user is logged in.
        /// </summary>
        private bool _isGuestAccount;
        
        /// <summary>
        ///  TODO: Add documentation.
        /// </summary>
        [NonSerialized] private Request _updateRequest;
        
        /// <summary>
        ///  The contacts list is used to store the contacts of the user.
        /// </summary>
        [NonSerialized] private ContactList<IUser> _contacts;
        [NonSerialized] private List<ContactRequestData> _sentRequests;
        [NonSerialized] private List<ContactRequestData> _receivedRequests;
        [NonSerialized] private List<MessageData> _loadedMessages;
        [NonSerialized] private List<Conversation> _loadedConversations;
        [NonSerialized] private List<ItemData> _items;

        public UserData(string userName = null, string email = null, string password = null, string firstName = null, string middleName = null, string userToken = null, string lastName = null, string phone = null, string language = null, List<CustomFieldList> customUserData = null)
        {
            _id = -1;
            _userName = userName ?? LocalAccountController.GenerateUserName();
            _email = email ?? _userName;
            _password = password ?? LocalAccountController.GenerateComplexPassword();
            _firstName = firstName;
            _middleName = middleName;
            _userToken = userToken;
            _lastName = lastName;
            _phone = phone;
            _coinBalance = 0;
            _clanId = -1;
            _localTimeZone = TimeZoneInfo.Local.DisplayName;
            _localTimeZoneOffset = TimeZoneInfo.Local.BaseUtcOffset.TotalMilliseconds;
            _language = language;
            _created = DateTime.UtcNow;
            _lastUpdated = DateTime.UtcNow;
            _status = -1;
            _customUserData = customUserData;
        }

        /// <summary>
        /// Only for Reflection purposes. DON'T USE THIS CONSTRUCTOR!
        /// </summary>
        public UserData()
        {
            _id = -1;
            _userName = "Guest" + DateTime.UtcNow.Ticks + "@guest.com";
            _email = _userName;
            _password = "guest";
            _firstName = null;
            _middleName = null;
            _userToken = null;
            _lastName = null;
            _phone = null;
            _coinBalance = 0;
            _clanId = -1;
            _localTimeZone = TimeZoneInfo.Local.DisplayName;
            _localTimeZoneOffset = TimeZoneInfo.Local.BaseUtcOffset.TotalMilliseconds;
            _language = null;
            _created = DateTime.UtcNow;
            _lastUpdated = DateTime.UtcNow;
            _status = -1;
            _customUserData = null;
        }

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set => _isLoggedIn = value;
        }

        public bool IsGuestAccount
        {
            get => _isGuestAccount;
            set => _isGuestAccount = value;
        }

        public List<CustomFieldList> CustomUserData
        {
            get => _customUserData;
            set => _customUserData = value;
        }

        public int ID
        {
            get => _id;
            set => _id = value;
        }

        public string UserName
        {
            get => _userName;
            set => _userName = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string Password
        {
            get => _password;
            set => _password = value;
        }

        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string MiddleName
        {
            get => _middleName;
            set => _middleName = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string Phone
        {
            get => _phone;
            set => _phone = value;
        }

        public int CoinBalance
        {
            get => _coinBalance;
            set => _coinBalance = value;
        }

        public int ClanId
        {
            get => _clanId;
            set => _clanId = value;
        }

        public string LocalTimeZone
        {
            get => _localTimeZone;
            set => _localTimeZone = value;
        }

        public double LocalTimeZoneOffset
        {
            get => _localTimeZoneOffset;
            set => _localTimeZoneOffset = value;
        }

        public string Language
        {
            get => _language;
            set => _language = value;
        }

        public DateTime Created
        {
            get => _created;
            set => _created = value;
        }

        public DateTime LastUpdated
        {
            get => _lastUpdated;
            set => _lastUpdated = value;
        }

        public int Status
        {
            get => _status;
            set => _status = value;
        }
        public string UserToken
        {
            get => _userToken;
            set => _userToken = value;
        }
        public ContactList<IUser> Contacts
        {
            get => _contacts;
            set => _contacts = value;
        }
        public List<ContactRequestData> SentRequests
        {
            get => _sentRequests;
            set => _sentRequests = value;
        }
        public List<ContactRequestData> ReceivedRequests
        {
            get => _receivedRequests;
            set => _receivedRequests = value;
        }
        public List<MessageData> LoadedMessages
        {
            get => _loadedMessages;
            set => _loadedMessages = value;
        }
        public List<ItemData> Items
        {
            get => _items;
            set => _items = value;
        }
        public List<Conversation> LoadedConversations
        {
            get => _loadedConversations;
            set => _loadedConversations = value;
        }

        public Request UpdateRequest => _updateRequest;
        public void CopyTokenFromRequest(IRequest requestToCopy)
        {
            _userToken = requestToCopy.Token;
        }

        public async Task<bool> UpdateUserData(Action<IRequest> updateUserCallback)
        {
            IRequest request = ServerRequestGetterService.Get(RequestType.UpdateUserRequest, true, true);
            await ServiceHandler.Locator.Get<ServerRequestSenderService>().SendRequest(request, updateUserCallback);
            return false;
        }
    }
}