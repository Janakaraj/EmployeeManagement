using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Models
{
    public class DataInitializer
    {
        public static void SeedData
(UserManager<IdentityUser> userManager,
RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers
    (UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("admin@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@gmail.com";
                user.Email = "admin@gmail.com";
                IdentityResult result = userManager.CreateAsync
                (user, "admin@12345A").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }


            if (userManager.FindByNameAsync
        ("hr@gmail.com").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "hr@gmail.com";
                user.Email = "hr@gmail.com";

                IdentityResult result = userManager.CreateAsync
                (user, "hr@12345A").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,"HR").Wait();
                }
            }
        }

        public static void SeedRoles
    (RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("HR").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "HR";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Employee").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Employee";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
