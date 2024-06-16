using System;
using System.Collections.Generic;
using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Enums;
using UserSystemFramework.Scripts.System.Data.Interfaces;

namespace UserSystemFramework.Scripts.System.Data.Classes
{
    [Serializable]
    public class UserContactList<T> : ContactList<T> where T : IUser
    {
        public IUser User;
        public UserContactList(IUser user, Dictionary<T, ContactStatus> userContacts) : base(userContacts)
        {
            User = user;
            base.UserContacts = userContacts;
        }
    }
}