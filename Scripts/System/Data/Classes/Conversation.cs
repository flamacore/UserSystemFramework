using System.Collections.Generic;
using UserSystemFramework.Scripts.System.Data.Interfaces;

namespace UserSystemFramework.Scripts.System.Data.Classes
{
    public class Conversation
    {
        public IUser Sender;
        public IUser Receiver;
        public List<MessageData> Messages;

        public Conversation(IUser sender, IUser receiver, List<MessageData> messages)
        {
            Sender = sender;
            Receiver = receiver;
            Messages = messages;
        }
    }
}