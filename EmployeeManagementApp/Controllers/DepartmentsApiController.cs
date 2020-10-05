using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementApp.Models;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagementApp.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public DepartmentsApiController(AppDbContext context, UserManager<IdentityUser> userManager, IHubContext<NotificationHub> notificationHubContext)
        {
            _context = context;
            this._userManager = userManager;
            this._notificationHubContext = notificationHubContext;
        }


        // GET: api/DepartmentsApi
        [Authorize(Roles = "Admin, HR, Employee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> Getdepatments()
        {
            return await _context.depatments.ToListAsync();
        }

        // GET: api/DepartmentsApi/5
        [Authorize(Roles = "Admin, HR")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await _context.depatments.FindAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/DepartmentsApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            _context.Entry(department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // POST: api/DepartmentsApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            _context.depatments.Add(department);
            await _context.SaveChangesAsync();
            var groups = "HR";
            var name = department.DepartmentName;
            //await this._notificationHubContext.Clients.Group(groups).SendAsync("sendAddDepartmentMessage", name);
            await this._notificationHubContext.Clients.All.SendAsync("sendAddDepartmentMessage", name);
            return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
        }

        // DELETE: api/DepartmentsApi/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            var department = await _context.depatments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            var toDeleteEmailList = _context.employees.Where(e => e.DepartmentId == id).ToList();
            foreach( var u in toDeleteEmailList)
            {
                var user = await _userManager.FindByEmailAsync(u.Email);
                await _userManager.DeleteAsync(user);
                _context.employees.Remove(u);
            }

            _context.depatments.Remove(department);
            await _context.SaveChangesAsync();

            return department;
        }

        private bool DepartmentExists(int id)
        {
            return _context.depatments.Any(e => e.DepartmentId == id);
        }
    }
}
