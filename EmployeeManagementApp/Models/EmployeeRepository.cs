using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Models
{
    public class EmployeeRepository:IEmployeeRepository
    {
       public List<Employee> GetEmployees()
        {
            return EmployeeList.GetEmployees();
        }
        public Employee GetEmployeeById(int id)
        {
            return EmployeeList.GetEmployees().Find(emp => emp.Id == id); 
        }
        public Employee AddEmployee(Employee employee)
        {
            return EmployeeList.AddToList(employee);
        }
        public Employee EditEmployee(int id, Employee employee)
        {
            return EmployeeList.EditInList(id, employee);
        }
        public void DeleteEmployee(int id)
        {
            EmployeeList.DeleteInList(id);
        }
    }
}
