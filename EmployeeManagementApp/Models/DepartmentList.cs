using System.Collections.Generic;
using System.Linq;
using Dapper;
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


            string query = "Select * from dbo.Department";
            departmentList.Clear();
            var departments = con.Query<Department>(query);
            foreach(Department dept in departments)
            {
                Department department = new Department();
                department.DepartmentId = dept.DepartmentId;
                department.DepartmentName = dept.DepartmentName;
                departmentList.Add(department);
            }
            return departmentList;
        }
        public static Department AddToList(Department department)
        {
            string query = "INSERT INTO dbo.Department(DepartmentName) VALUES("+"'"+ department.DepartmentName+"'"+")";
            con.Execute(query);
            return department;
        }
        public static Department EditInList(int id, Department department)
        {
            departmentList.Clear();
            string query = "UPDATE dbo.Department SET DepartmentName = '" + department.DepartmentName + "' WHERE DepartmentId = " + id;
            con.Execute(query);

            return department;
        }
        public static void DeleteInList(int id)
        {
            departmentList.Clear();
            string query = "DELETE FROM dbo.Department WHERE DepartmentId = " + id;
            con.Execute(query);
        }
    }
}