using System;
using UserSystemFramework.Scripts.System.Data.Interfaces;

namespace UserSystemFramework.Scripts.System.Data.Classes
{
    public class TradeItemData : IDatabaseEntry
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _itemId;
        public int ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

        private int _price;
        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        private int _sellingUserId;
        public int SellingUserId
        {
            get { return _sellingUserId; }
            set { _sellingUserId = value; }
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
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}