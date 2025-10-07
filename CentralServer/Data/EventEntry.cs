using System;

namespace CentralServer.Data
{
    public class EventEntry
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TargetDepartments { get; set; }
        public string Severity { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public object NACS_ID { get; internal set; }
    }

}
