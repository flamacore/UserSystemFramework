using System;
using UserSystemFramework.Scripts.API.Attributes;
using UserSystemFramework.Scripts.API.Interfaces;
using UserSystemFramework.Scripts.System.Data.Interfaces;

namespace UserSystemFramework.Scripts.System.Data.Classes
{
    public class ItemData : IDatabaseEntry
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _itemName;
        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _shortDescription;
        public string ShortDescription
        {
            get { return _shortDescription; }
            set { _shortDescription = value; }
        }

        private int _itemType;
        public int ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }

        private int _itemRarity;
        public int ItemRarity
        {
            get { return _itemRarity; }
            set { _itemRarity = value; }
        }

        private int _stackable;
        public int Stackable
        {
            get { return _stackable; }
            set { _stackable = value; }
        }

        private int _stackLimit;
        public int StackLimit
        {
            get { return _stackLimit; }
            set { _stackLimit = value; }
        }

        private int _tradable;
        public int Tradable
        {
            get { return _tradable; }
            set { _tradable = value; }
        }

        private int _consumable;
        public int Consumable
        {
            get { return _consumable; }
            set { _consumable = value; }
        }

        private int _equippable;
        public int Equippable
        {
            get { return _equippable; }
            set { _equippable = value; }
        }

        private DateTime _created;
        public DateTime Created
        {
            get { return _created; }
            set { _created = value; }
        }

        private DateTime _lastUpdated;
        public DateTime LastUpdated
        {
            get { return _lastUpdated; }
            set { _lastUpdated = value; }
        }

        private int _status;
        private int _id1;
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}