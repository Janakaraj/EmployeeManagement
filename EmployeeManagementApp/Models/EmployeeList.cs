using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
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
            Department department = new Department();
            employeeList = new List<Employee>();
            con = new SqlConnection(connectionString);
        }
        public static List<Employee> GetEmployees()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from dbo.Employee inner join  dbo.Department on Employee.DepartmentId = Department.DepartmentId", con);
            SqlDataReader reader = cmd.ExecuteReader();
            employeeList.Clear();
            while (reader.Read())
            {
                Employee employee = new Employee();
                employee.Id = Convert.ToInt32(reader[0]);
                employee.Name = reader[1].ToString();
                employee.Surname = reader[2].ToString();
                employee.Address = reader[3].ToString();
                employee.Qualification = reader[4].ToString();
                employee.ContactNumber = long.Parse(Convert.ToString(reader[5]));
                employee.DepartmentId = Convert.ToInt32(reader[6]);
                employee.department = new Department();
                employee.department.DepartmentId = Convert.ToInt32(reader[7]);
                employee.department.DepartmentName = reader[8].ToString();
                employeeList.Add(employee);
            }
            con.Close();
            return employeeList;
        }
        public static Employee AddToList(Employee employee)
        {
            //employeeList.Add(employee);
            con.Open();
            string query = "INSERT INTO dbo.Employee(Name, Surname, Address, Qualification, ContactNumber, DepartmentId) VALUES(@Name, @Surname, @Address, @Quali, @Cn, @Dept)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Surname", employee.Surname);
            cmd.Parameters.AddWithValue("@Address", employee.Address);
            cmd.Parameters.AddWithValue("@Quali", employee.Qualification);
            cmd.Parameters.AddWithValue("@Cn", employee.ContactNumber);
            cmd.Parameters.AddWithValue("@Dept", employee.DepartmentId);
            cmd.ExecuteNonQuery();
            con.Close();
            return employee;
        }
        public static Employee EditInList(int id, Employee employee)
        {
            employeeList.Clear();
            con.Open();
            string query = "UPDATE dbo.Employee SET Name = '" + employee.Name + "', Surname = '" + employee.Surname + "', Address = '" + employee.Address + "', Qualification = '" + employee.Qualification + "', ContactNumber = '" + employee.ContactNumber + "', DepartmentId = '" + employee.DepartmentId + "' WHERE Id = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            //Employee employeeToEdit = employeeList.Find(x => x.Id == id);
            //employeeToEdit.Name = employee.Name;
            //employeeToEdit.Surname = employee.Surname;
            //employeeToEdit.Address = employee.Address;
            //employeeToEdit.ContactNumber = employee.ContactNumber;
            //employeeToEdit.Department = employee.Department;
            //employeeToEdit.Qualification = employee.Qualification;
            return employee;
        }
        public static void DeleteInList(int id)
        {
            //Employee employeeToDelete = employeeList.Find(x => x.Id == id);
            //employeeList.Remove(employeeToDelete);
            //return employeeToDelete;
            employeeList.Clear();
            con.Open();
            string query = "DELETE FROM dbo.Employee WHERE Id = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}