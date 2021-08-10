using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheJudgesystem.Data.Models;

namespace TheJudgesystem.Services.Data
{
    public interface IUsersService
    {
        public Task SetRole(string role, ClaimsPrincipal user);

        public Task<ApplicationUser> GetUser(ClaimsPrincipal user);

        public string GetApplicationUserId(ClaimsPrincipal user);

        public string GetApplicaionUserRole(ClaimsPrincipal user);

        public Task<bool> IsInThisRole(string role, ClaimsPrincipal user);
    }
}
