using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using EmployeeManagementApp.Hubs;

namespace EmployeeManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public EmployeesApiController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IHubContext<NotificationHub> notificationHubContext)
        {
            this._context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._notificationHubContext = notificationHubContext;
        }

        // GET: api/EmployeesApi
        [Authorize(Roles = "Admin, HR, Employee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Getemployees()
        {
            if (User.IsInRole("Admin") || User.IsInRole("HR"))
            {
                return await _context.employees.Include(e => e.department).ToListAsync();
            }
            else if(User.IsInRole("Employee"))
            {
                var dept = _context.employees.Include(e => e.department).Where(e => e.Email == User.FindFirst("name").Value).First().department.DepartmentName;
                var sameDeptEmployee = _context.employees.Include(e => e.department).Where(e => e.department.DepartmentName == dept);
                return await sameDeptEmployee.ToListAsync();
            }
            return await _context.employees.Include(e => e.department).ToListAsync();
        }

        // GET: api/EmployeesApi/5
        [Authorize(Roles = "Admin, HR, Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            var department = await _context.depatments.FindAsync(employee.DepartmentId);
            employee.department = department;

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/EmployeesApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "Admin, HR, Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await this._notificationHubContext.Clients.All.SendAsync("sendEditProfileMessage", employee.Name);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EmployeesApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "Admin, HR")]
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.employees.Add(employee); 
            string password = employee.Name.ToString() + "@12345A";
            var userName = employee.Email;
            var userEmail = employee.Email;
            var role = await _roleManager.RoleExistsAsync("Employee");
            var user = new IdentityUser { UserName = userName, Email = userEmail };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Employee");

                await _context.SaveChangesAsync();
                var name = employee.Name;
                var surname = employee.Surname;
                var dept = _context.depatments.Where(d => d.DepartmentId == employee.DepartmentId).First().DepartmentName;
                var grpName = "Employee" + dept;
                await this._notificationHubContext.Clients.All.SendAsync("sendAddEmployeeMessage", name, surname);
                await this._notificationHubContext.Clients.All.SendAsync("send","hello from the server");
            }
            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/EmployeesApi/5
        [Authorize(Roles = "Admin, HR")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            var user = await _userManager.FindByEmailAsync(employee.Email);
            await _userManager.DeleteAsync(user);
            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        private bool EmployeeExists(int id)
        {
            return _context.employees.Any(e => e.Id == id);
        }
    }
}
