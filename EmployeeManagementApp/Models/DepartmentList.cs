using System.Collections.Generic;
using System.Linq;
namespace EmployeeManagementApp.Models
{
    internal class DepartmentList
    {
        static List<Department> departmentList = null;
        static DepartmentList()
        {
            departmentList = new List<Department>()
            {
                new Department(){DepartmentId=0,DepartmentName="HR"}
        };
        }
        public static List<Department> GetDepartments()
        {
            return departmentList;
        }
        public static Department AddToList(Department department)
        {
            departmentList.Add(department);
            return department;
        }
        public static Department EditInList(int id, Department department)
        {
            Department departmentToEdit = departmentList.Find(x => x.DepartmentId == id);
            departmentToEdit.DepartmentName = department.DepartmentName;
            
            return department;
        }
        public static Department DeleteInList(int id)
        {
            Department departmentToDelete = departmentList.Find(x => x.DepartmentId == id);
            departmentList.Remove(departmentToDelete);
            return departmentToDelete;
        }
    }
}