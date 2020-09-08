using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.Models
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> employees { get; set; }
        public DbSet<Department> depatments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>().HasData(
               new Department { DepartmentId = 1, DepartmentName = "admin" });
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Email = "admin@gmail.com", Name = "admin", Surname = "admin", Address = "Vadodara", ContactNumber = 12345667890, Qualification = "BE", DepartmentId = 1 });
            modelBuilder.Entity<Employee>().HasData(
                 new Employee { Id = 2, Email = "hr@gmail.com", Name = "hr", Surname = "hr", Address = "Vadodara", ContactNumber = 12345667890, Qualification = "BE", DepartmentId = 1 });

        }
    }
}
