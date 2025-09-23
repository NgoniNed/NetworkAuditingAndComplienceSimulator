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
    }
    public class Department
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; } = "Active";
    }
    public class EventEntry
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TargetDepartments { get; set; }
        public string Severity { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }

}
