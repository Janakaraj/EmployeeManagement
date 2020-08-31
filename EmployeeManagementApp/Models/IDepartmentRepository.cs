using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Models
{
    interface IDepartmentRepository
    {
        List<Department> GetDepartments();
        Department GetDepartmentById(int dId);
        Department AddDepartment(Department dept);
        Department DeleteDepartment(int dId);
        Department EditDepartment(int dId, Department dept);
    }
}
