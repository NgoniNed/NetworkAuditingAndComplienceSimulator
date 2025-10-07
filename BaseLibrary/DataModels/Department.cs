namespace BaseLibrary.DataModels
{
    public class Department
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; } = "Active";
        public string Port
        {
            get;
            set;
        }
        public string URL
        {
            get;
            set;
        }
        public DepartmentStatus DepartmentStatus
        {
            get;
            set;
        }
        public object NACS_ID { get; set; }
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
