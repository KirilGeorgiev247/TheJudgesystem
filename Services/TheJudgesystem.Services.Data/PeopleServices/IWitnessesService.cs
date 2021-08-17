using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Web.ViewModels.Witnesses;

namespace TheJudgesystem.Services.Data.PeopleServices
{
    public interface IWitnessesService
    {

        public Task<int> GetCasesCount(ClaimsPrincipal user);

        public Task<Witness> GetWitness(ClaimsPrincipal user);

        public Task<ICollection<CaseInList>> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4);

        public Task AddIndication(IndicationInputModel input, int caseId, ClaimsPrincipal user);

    }
}
