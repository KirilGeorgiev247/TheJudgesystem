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

        public int GetCasesCount();

        public Lawyer GetLawyer(ClaimsPrincipal user);

        public IEnumerable<CaseInList> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4);

        public Task AddDefence(DefenceInputModel input, int caseId);

    }
}
