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
        public int GetCasesCount();

        public Prosecutor GetProsecutor(ClaimsPrincipal user);

        public IEnumerable<CaseInList> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4);

        public Task DecideForGuilty(DecisionInputModel input, int caseId, ClaimsPrincipal user);

        public Task DecideForNotGuilty(DecisionInputModel input, int caseId, ClaimsPrincipal user);

        public Task DecideForFee(DecisionInputModel input, int caseId, ClaimsPrincipal user);
    }
}
