using System;

namespace CentralServer.Data
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public object NACS_ID { get; internal set; }
    }

}
