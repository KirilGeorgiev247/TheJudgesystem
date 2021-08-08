using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Web.ViewModels.Witnesses;

namespace TheJudgesystem.Services.Data.PeopleServices
{
    public interface IWitnessesService
    {

        public int GetCasesCount();

        public Witness GetWitness(ClaimsPrincipal user);

        public IEnumerable<CaseInList> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4);

    }
}
