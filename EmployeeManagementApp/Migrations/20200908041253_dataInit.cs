using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementApp.Migrations
{
    public partial class dataInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "depatments",
                columns: new[] { "DepartmentId", "DepartmentName" },
                values: new object[] { 1, "admin" });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "Id", "Address", "ContactNumber", "DepartmentId", "Email", "Name", "Qualification", "Surname" },
                values: new object[] { 1, "Vadodara", 12345667890L, 1, "admin@gmail.com", "admin", "BE", "admin" });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "Id", "Address", "ContactNumber", "DepartmentId", "Email", "Name", "Qualification", "Surname" },
                values: new object[] { 2, "Vadodara", 12345667890L, 1, "hr@gmail.com", "hr", "BE", "hr" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "depatments",
                keyColumn: "DepartmentId",
                keyValue: 1);
        }
    }
}
