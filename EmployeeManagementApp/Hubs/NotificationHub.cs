using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementApp.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.Hubs
{
    public class NotificationHub:Hub
    {
        private readonly AppDbContext _context;
        public NotificationHub(AppDbContext context)
        {
            this._context = context;

        }
        public async Task SendAddEmployeeMessage(string name, string surname)
        {
            string dept = _context.employees.Include(e => e.department).Where(e => e.Email == this.Context.User.Identity.Name).First().department.DepartmentName;
            var grpName = "Employee" + dept;
            await Clients.Group(grpName).SendAsync("RecieveAddEmployeeMessage",name,surname);
        }
        public async Task SendEditProfileMessage(string name)
        {
            var group1 = "Admin";
            var group2 = "HR";
            await Clients.Groups(group1, group2).SendAsync("RecieveEditProfileMessage", name);
        }
        public async Task SendAddDepartmentMessage(string name)
        {
            var groups = "HR";
            await Clients.Group(groups).SendAsync("RecieveAddDepartmentMessage", name);
        }
        public override async Task OnConnectedAsync()
        {
            if (this.Context.User.IsInRole("Admin"))
            {
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, "Admin");
            }
            else if (this.Context.User.IsInRole("HR"))
            {
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, "HR");
            }
            else if (this.Context.User.IsInRole("Employee"))
            {
                string dept = _context.employees.Include(e => e.department).Where(e => e.Email == this.Context.User.Identity.Name).First().department.DepartmentName;
                var grpName = "Employee" + dept;
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, grpName);
                
            }
            await base.OnConnectedAsync();
        }
    }
}
