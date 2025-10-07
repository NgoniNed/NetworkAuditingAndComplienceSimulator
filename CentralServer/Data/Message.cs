using System;
namespace CentralServer.Data
{
    public class Message
    {
        public string SenderDepartment
        {
            get;
            set;
        } 
        public string RecipientDepartment
        {
            get;
            set;
        }
        public string MessageContent
        {
            get;
            set;
        }
        public MessageType Type
        {
            get;
            set;
        }
        public DateTime Timestamp
        {
            get;
            set;
        } = DateTime.UtcNow;
        public object NACS_ID { get; internal set; }
    }
}
