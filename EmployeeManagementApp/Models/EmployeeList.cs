using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagementApp.Models
{
    internal class EmployeeList
    {
        static List<Employee> employeeList = null;
        static EmployeeList()
        {
            employeeList = new List<Employee>()
            {
                new Employee(){Id=0,Name="Janak",Surname="Poojary",Address="Vadodara", ContactNumber=8160551685, Department="Development", Qualification="BE"}
        };
        }
        public static List<Employee> GetEmployees()
        {
            return employeeList;
        }
        public static Employee AddToList(Employee employee)
        {
            employeeList.Add(employee);
            return employee;
        }
        public static Employee EditInList(int id, Employee employee)
        {
            Employee employeeToEdit = employeeList.Find(x => x.Id == id);
            employeeToEdit.Name = employee.Name;
            employeeToEdit.Surname = employee.Surname;
            employeeToEdit.Address = employee.Address;
            employeeToEdit.ContactNumber = employee.ContactNumber;
            employeeToEdit.Department = employee.Department;
            employeeToEdit.Qualification = employee.Qualification;
            return employee;
        }
        public static Employee DeleteInList(int id)
        {
            Employee employeeToDelete = employeeList.Find(x => x.Id == id);
            employeeList.Remove(employeeToDelete);
            return employeeToDelete;
        }
    }
}