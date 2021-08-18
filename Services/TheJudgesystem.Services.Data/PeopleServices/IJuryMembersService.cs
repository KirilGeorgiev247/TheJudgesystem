using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Web.ViewModels.Jurymembers;

namespace TheJudgesystem.Services.Data.PeopleServices
{
    public interface IJurymembersService
    {

        public Task<int> GetCasesCount(ClaimsPrincipal user);

        public Task<Jurymember> GetJuryMember(ClaimsPrincipal user);

        public Task<ICollection<CaseInList>> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4);

        public Task AddOpinion(OpinionInputModel input, int caseId, ClaimsPrincipal user);

    }
}
