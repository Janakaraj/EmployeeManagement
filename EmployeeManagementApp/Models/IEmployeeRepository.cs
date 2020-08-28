using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Models
{
    interface IEmployeeRepository
    {
        List<Employee> GetEmployees();
        Employee GetEmployeeById(int id);
        Employee AddEmployee(Employee employee);
        void DeleteEmployee(int id);
        Employee EditEmployee(int id, Employee employee);
    }
}
