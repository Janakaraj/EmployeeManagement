using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [StringLength(20, ErrorMessage = "Name must be between 1 and 100 chars")]
        [Required]
        public string DepartmentName { get; set; }
    }
}
