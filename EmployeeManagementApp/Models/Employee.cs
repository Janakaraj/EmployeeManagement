using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Name must be between 1 and 20 chars")]
        
        public string Name { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Name must be between 1 and 20 chars")]
        

        public string Surname { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name must be between 1 and 100 chars")]
        
        public string Address { get; set; }
        public string Qualification { get; set; }
        [Required]
        [RegularExpression("^[0-9]*$",
         ErrorMessage = "Characters are not allowed.")]

        public long ContactNumber { get; set; }
        public int DepartmentId { get; set; }
        public Department department { get; set; }
    }
}
