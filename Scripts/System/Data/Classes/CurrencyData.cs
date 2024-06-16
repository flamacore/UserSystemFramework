using System;
using UserSystemFramework.Scripts.API.Attributes;
using UserSystemFramework.Scripts.API.Interfaces;
using UserSystemFramework.Scripts.System.Data.Interfaces;

namespace UserSystemFramework.Scripts.System.Data.Classes
{
    public class CurrencyData : IDatabaseEntry
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
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
        private string _currencyName;
        public string CurrencyName
        {
            get { return _currencyName; }
            set { _currencyName = value; }
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
        private int _category;
        public int Category
        {
            get { return _category; }
            set { _category = value; }
        }
        private int _status;
        private DateTime _created;
        private DateTime _lastUpdated;
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}