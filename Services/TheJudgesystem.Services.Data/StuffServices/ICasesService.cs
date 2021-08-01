using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheJudgesystem.Web.ViewModels.Cases;

namespace TheJudgesystem.Services.Data.StuffServices
{
    public interface ICasesService
    {

        public Task AddCase(CaseInputModel input, ClaimsPrincipal user);

    }
}
