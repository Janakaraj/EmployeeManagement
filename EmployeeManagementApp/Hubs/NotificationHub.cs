using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagementApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementApp.Hubs
{
    public class NotificationHub:Hub
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> userManager;
        public NotificationHub(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            this._context = context;
            this.userManager = userManager;

        }
        public async Task sendAddEmployeeMessage(string name, string surname)
        {
            string dept = _context.employees.Include(e => e.department).Where(e => e.Email == this.Context.User.Identity.Name).First().department.DepartmentName;
            var grpName = "Employee" + dept;
            await Clients.All.SendAsync("sendAddEmployeeMessage",name,grpName);
        }
        public async Task sendEditProfileMessage(string name)
        {
            var group1 = "Admin";
            var group2 = "HR";
            await Clients.Groups(group1, group2).SendAsync("sendEditProfileMessage", name);
        }
        public async Task sendAddDepartmentMessage(string name)
        {
            var groups = "HR";
            await Clients.All.SendAsync("sendAddDepartmentMessage", this.Context.User);
        }
        public override async Task OnConnectedAsync()
        {
            if (this.Context.GetHttpContext().User.IsInRole("Admin"))
            {
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, "Admin");
            }
            else if (this.Context.GetHttpContext().User.IsInRole("HR"))
            {
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, "HR");
            }
            else if (this.Context.User.FindFirst("role")?.Value == "Employee")
            {
                string dept = _context.employees.Include(e => e.department).Where(e => e.Email == this.Context.User.Identity.Name).First().department.DepartmentName;
                var grpName = "Employee" + dept;
                await this.Groups.AddToGroupAsync(this.Context.ConnectionId, grpName);
                
            }
            await base.OnConnectedAsync();
        }
    }
}
