using System;
using UserSystemFramework.Scripts.System.Data.Interfaces;

namespace UserSystemFramework.Scripts.System.Data.Classes
{
    public class ItemPropertyData : IDatabaseEntry
    {
        private int _id;
        private DateTime _created;
        private DateTime _lastUpdated;
        private int _status;
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
    }
}