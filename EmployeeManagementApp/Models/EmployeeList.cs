using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using System;   
namespace EmployeeManagementApp.Models
{
    public class EmployeeList
    {
        static SqlConnection con;
        static List<Employee> employeeList = null;
        static EmployeeList()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmployeeManagementDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            employeeList = new List<Employee>();
            con = new SqlConnection(connectionString);
        }
        public static List<Employee> GetEmployees()
        {
            employeeList.Clear();
            var employees = con.Query<EmployeeDataList>("select * from dbo.employee inner join  dbo.department on employee.departmentid = department.departmentid");
            foreach (var emp in employees)
            {
                Employee employee = new Employee();
                employee.Id = Convert.ToInt32(emp.Id);
                employee.Name = emp.Name.ToString();
                employee.Surname = emp.Surname.ToString();
                employee.Address = emp.Address.ToString();
                employee.Qualification = emp.Qualification.ToString();
                employee.ContactNumber = long.Parse(Convert.ToString(emp.ContactNumber));
                employee.DepartmentId = Convert.ToInt32(emp.DepartmentId);
                employee.department = new Department();
                employee.department.DepartmentId = Convert.ToInt32(emp.DepartmentId);
                employee.department.DepartmentName = emp.DepartmentName.ToString();
                employeeList.Add(employee);
            }
            return employeeList;
        }
        public static Employee AddToList(Employee employee)
        {
            employeeList.Add(employee);
            string query = "INSERT INTO dbo.Employee(Name, Surname, Address, Qualification, ContactNumber, DepartmentId) VALUES(" + "'" + employee.Name + "'" + ","
                + "'" + employee.Surname + "'" + "," + "'" + employee.Address + "'" + "," + "'" + employee.Qualification + "'" + "," + "'" + employee.ContactNumber + "'" + "," + "'" + employee.DepartmentId + "'" + ")";
            con.Execute(query);
            return employee;
        }
        public static Employee EditInList(int id, Employee employee)
        {
            employeeList.Clear();
            string query = "UPDATE dbo.Employee SET Name = '" + employee.Name + "', Surname = '" + employee.Surname + "', Address = '" + employee.Address + "', Qualification = '" + employee.Qualification + "', ContactNumber = '" + employee.ContactNumber + "', DepartmentId = '" + employee.DepartmentId + "' WHERE Id = " + id;
            con.Execute(query);
            
            return employee;
        }
        public static void DeleteInList(int id)
        {
            employeeList.Clear();
            string query = "DELETE FROM dbo.Employee WHERE Id = " + id;
            con.Execute(query);
        }
    }
}