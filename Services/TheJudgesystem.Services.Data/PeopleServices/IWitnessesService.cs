﻿using System;
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

        public Task<int> GetCasesCount();

        public Task<Witness> GetWitness(ClaimsPrincipal user);

        public Task<ICollection<CaseInList>> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4);

    }
}
