using System;
using UserSystemFramework.Scripts.System.Data.Interfaces;

namespace UserSystemFramework.Scripts.System.Data.Classes
{
    public class ContactRequestData : IDatabaseEntry
    {
        private int _id;
        private DateTime _created;
        private DateTime _lastUpdated;
        private int _status;
        private int _relationshipStatus;
        private int _userId;
        private int _friendId;
        
        public int UserId
        {
            get => _userId;
            set => _userId = value;
        }
        public int ID
        {
            get => _id;
            set => _id = value;
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
        public int RelationshipStatus
        {
            get => _relationshipStatus;
            set => _relationshipStatus = value;
        }
        public int FriendId
        {
            get => _friendId;
            set => _friendId = value;
        }
    }
}