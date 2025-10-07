namespace CentralServer.Data
{
    public class Department
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; } = "Active";
        public DepartmentStatus DepartmentStatus
        {
            get;
            set;
        }
    }
    public enum DepartmentStatus
    {
        Active,
        NonActive,
        Suspended,
        Banned,
        Probation
    }
}
