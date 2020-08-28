using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System;

namespace EmployeeManagementApp.Models
{
    public class DepartmentList
    {
        static SqlConnection con;
        public static List<Department> departmentList = null;
        static DepartmentList()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmployeeManagementDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            departmentList = new List<Department>() { new Department() { DepartmentId = 0, DepartmentName = "test" } };
            con = new SqlConnection(connectionString);

        }
        public static List<Department> GetDepartments()
        {
            
            con.Open();
            SqlCommand cmd = new SqlCommand("Select * from dbo.Department", con);
            SqlDataReader reader = cmd.ExecuteReader();
            departmentList.Clear();
            while (reader.Read())
            {
                Department department = new Department();
                department.DepartmentId = Convert.ToInt32(reader[0]);
                department.DepartmentName = reader[1].ToString();
                departmentList.Add(department);
            }
            con.Close();
            return departmentList;
        }
        public static Department AddToList(Department department)
        {
            //departmentList.Add(department);
            con.Open();
            string query = "INSERT INTO dbo.Department(DepartmentName) VALUES(@Name)";
            SqlCommand cmd = new SqlCommand(query, con);  
            cmd.Parameters.AddWithValue("@Name", department.DepartmentName);
            cmd.ExecuteNonQuery();
            con.Close();
            return department;
        }
        public static Department EditInList(int id, Department department)
        {
            //Department departmentToEdit = departmentList.Find(x => x.DepartmentId == id);
            //departmentToEdit.DepartmentName = department.DepartmentName;
            departmentList.Clear();
            con.Open();
            string query = "UPDATE dbo.Department SET DepartmentName = '" + department.DepartmentName + "' WHERE DepartmentId = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();

            return department;
        }
        public static void DeleteInList(int id)
        {
            //Department departmentToDelete = departmentList.Find(x => x.DepartmentId == id);
            //departmentList.Remove(departmentToDelete);
            //return departmentToDelete;
            departmentList.Clear();
            con.Open();
            string query = "DELETE FROM dbo.Department WHERE DepartmentId = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}