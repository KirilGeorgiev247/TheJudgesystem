using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Web.ViewModels.Prosecutors;

namespace TheJudgesystem.Services.Data.PeopleServices
{
    public interface IProsecutorsService
    {
        public Task<int> GetCasesCount();

        public Task<Prosecutor> GetProsecutor(ClaimsPrincipal user);

        public Task<ICollection<CaseInList>> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4);

        public Task DecideForGuilty(DecisionInputModel input, int caseId, ClaimsPrincipal user);

        public Task DecideForNotGuilty(DecisionInputModel input, int caseId, ClaimsPrincipal user);

        public Task DecideForFee(DecisionInputModel input, int caseId, ClaimsPrincipal user);
    }
}
