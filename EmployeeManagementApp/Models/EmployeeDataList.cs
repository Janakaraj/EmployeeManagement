namespace EmployeeManagementApp.Models
{
    public class EmployeeDataList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Qualification { get; set; }
        public long ContactNumber { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}