using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using EmployeeManagementApp.Hubs;

namespace EmployeeManagementApp.Controllers
{
    
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public EmployeeController(AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IHubContext<NotificationHub> notificationHubContext)
        {
            this._context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._notificationHubContext = notificationHubContext;
        }

        // GET: Employee
        [Authorize(Roles = "Admin, HR, Employee")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.employees.Include(e => e.department);
            if (User.IsInRole("Admin") || User.IsInRole("HR"))
            {
                return View(await appDbContext.ToListAsync());
            }
            else
            {
                var dept = appDbContext.Where(e => e.Email == User.Identity.Name).First().department.DepartmentName;
                var sameDeptEmployee = appDbContext.Where(e => e.department.DepartmentName == dept);
                return View(await sameDeptEmployee.ToListAsync());
            }
        }

        // GET: Employee/Details/5
        [Authorize(Roles = "Admin, HR, Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employees
                .Include(e => e.department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        [Authorize(Roles = "Admin, HR")]
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.depatments, "DepartmentId", "DepartmentName");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, HR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Name,Surname,Address,Qualification,ContactNumber,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
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
                    //await this._notificationHubContext.Clients.All.SendAsync("SendMessage", name, surname);
                    return RedirectToAction(nameof(Index));
                    //return View();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            ViewData["DepartmentId"] = new SelectList(_context.depatments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        [Authorize(Roles = "Admin, HR, Employee")]
        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.depatments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, HR, Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Name,Surname,Address,Qualification,ContactNumber,DepartmentId")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.depatments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employee/Delete/5
        [Authorize(Roles = "Admin, HR")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.employees
                .Include(e => e.department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [Authorize(Roles = "Admin, HR")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.employees.FindAsync(id);
            var user = await _userManager.FindByEmailAsync(employee.Email);
            await _userManager.DeleteAsync(user);
            _context.employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.employees.Any(e => e.Id == id);
        }
    }
}
