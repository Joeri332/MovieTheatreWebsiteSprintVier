using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MovieTheatreDatabase.Context
{
    public static class UserAndRoleDataInitializer
    {
        public static async Task SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        private static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            //Proper implementation would check for existence to prevent unnecessary operation to database
            var admin = new IdentityUser { UserName = "Admin123@hotmail.com", Email = "Admin123@hotmail.com" };
            await userManager.CreateAsync(admin, "Admin123!");
            await userManager.CreateAsync(new IdentityUser { UserName = "Manager@hotmail.com", Email = "Manager@hotmail.com" }, "Manager123!");
            await userManager.CreateAsync(new IdentityUser { UserName = "Cashier@hotmail.com", Email = "Cashier@hotmail.com" }, "Cashier123!");
            await userManager.CreateAsync(new IdentityUser { UserName = "User@hotmail.com", Email = "User@hotmail.com" }, "User123!");

            await userManager.AddToRoleAsync(admin, "Admin");
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            //Proper implementation would check for existence to prevent unnecessary operation to database
            await roleManager.CreateAsync(new IdentityRole() {Name = "Admin"});
            await roleManager.CreateAsync(new IdentityRole() { Name = "Manager" });
            await roleManager.CreateAsync(new IdentityRole() { Name = "Cashier" });
            await roleManager.CreateAsync(new IdentityRole() { Name = "User" });
        }
    }
}