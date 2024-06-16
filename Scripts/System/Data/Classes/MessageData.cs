using System;
using UserSystemFramework.Scripts.System.Data.Enums;
using UserSystemFramework.Scripts.System.Data.Interfaces;

namespace UserSystemFramework.Scripts.System.Data.Classes
{
    
    public class MessageData: IDatabaseEntry
    {
        private int _id;
        private DateTime _created;
        private DateTime _lastUpdated;
        public MessageStatus messageStatus;
        private string _message;
        private int _fromUserID;
        private int _toUserID;
        private DateTime _sentTime;
        private DateTime _seenTime;
        public string Message
        {
            get => _message;
            set => _message = value;
        }
        public int FromUserID
        {
            get => _fromUserID;
            set => _fromUserID = value;
        }
        public int ToUserID
        {
            get => _toUserID;
            set => _toUserID = value;
        }
        public DateTime SentTime
        {
            get => _sentTime;
            set => _sentTime = value;
        }
        public DateTime SeenTime
        {
            get => _seenTime;
            set => _seenTime = value;
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
            get => (int)messageStatus;
            set => messageStatus = (MessageStatus)value;
        }
    }
}