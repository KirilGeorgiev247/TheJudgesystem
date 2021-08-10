using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheJudgesystem.Data.Models;

namespace TheJudgesystem.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UsersService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public string GetApplicationUserId(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public async Task SetRole(string role, ClaimsPrincipal user)
        {
            if (this.roleManager.Roles.Any(x => x.Name == role))
            {
                var currentUser = await this.GetUser(user);
                await this.userManager.AddToRoleAsync(currentUser, role);
            }
            else
            {
                await this.roleManager.CreateAsync(new ApplicationRole
                {
                    Name = role,
                });

                var currentUser = await this.GetUser(user);
                await this.userManager.AddToRoleAsync(currentUser, role);

            }
        }

        public async Task<ApplicationUser> GetUser(ClaimsPrincipal user)
        {
            var currentUser = await this.userManager.GetUserAsync(user);

            return currentUser;
        }

        public async Task<bool> IsInThisRole(string role, ClaimsPrincipal user)
        {
            var currentUser = await this.GetUser(user);

            if (currentUser == null)
            {
                return false;
            }

            var result = await this.userManager.IsInRoleAsync(currentUser, role);

            return result;
        }

    }
}
