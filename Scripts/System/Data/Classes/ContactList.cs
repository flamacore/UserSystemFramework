using System;
using System.Collections.Generic;
using UserSystemFramework.Scripts.System.Controllers.Classes;
using UserSystemFramework.Scripts.System.Data.Enums;
using UserSystemFramework.Scripts.System.Data.Interfaces;

namespace UserSystemFramework.Scripts.System.Data.Classes
{
    [Serializable]
    public class ContactList<T> where T : IUser
    {
        public Dictionary<T, ContactStatus> UserContacts;
        public ContactList(Dictionary<T, ContactStatus> userContacts)
        {
            this.UserContacts = userContacts;
        }

        public ContactList()
        {
            UserContacts = new Dictionary<T, ContactStatus>();
        }
    }
}