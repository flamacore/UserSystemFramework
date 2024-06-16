using System;
using UserSystemFramework.Scripts.System.Data.Interfaces;

namespace UserSystemFramework.Scripts.System.Data.Classes
{
    [Serializable]
    public class AchievementData : IDatabaseEntry
    {
        private int _id;
        private DateTime _created;
        private DateTime _lastUpdated;
        private int _status;
        private string _achievementName;
        private string _achievementDescription;
        private string _achievementShortDescription;
        private int _progress;
        
        public string AchievementDescription
        {
            get => _achievementDescription;
            set => _achievementDescription = value;
        }
        public string AchievementShortDescription
        {
            get => _achievementShortDescription;
            set => _achievementShortDescription = value;
        }
        public int Progress
        {
            get => _progress;
            set => _progress = value;
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
        public string AchievementName
        {
            get => _achievementName;
            set => _achievementName = value;
        }
    }
}