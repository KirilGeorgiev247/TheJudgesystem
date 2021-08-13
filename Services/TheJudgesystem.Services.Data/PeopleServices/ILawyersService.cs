using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Web.ViewModels.Lawyers;

namespace TheJudgesystem.Services.Data.PeopleServices
{
    public interface ILawyersService
    {

        public Task<int> GetCasesCount();

        public Task<Lawyer> GetLawyer(ClaimsPrincipal user);

        public Task<ICollection<CaseInList>> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4);

        public Task AddDefence(DefenceInputModel input, int caseId);

    }
}
