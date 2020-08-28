using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Models
{
    public class DepartmentRepository: IDepartmentRepository
    {
        public List<Department> GetDepartments()
        {
            return DepartmentList.GetDepartments();
        }
        public Department GetDepartmentById(int dId)
        {
            return DepartmentList.GetDepartments().Find(dept => dept.DepartmentId == dId);
        }
        public Department AddDepartment(Department department)
        {
            return DepartmentList.AddToList(department);
        }
        public Department EditDepartment(int dId, Department department)
        {
            return DepartmentList.EditInList(dId, department);
        }
        public void DeleteDepartment(int dId)
        {
           DepartmentList.DeleteInList(dId);
        }
    }
}
