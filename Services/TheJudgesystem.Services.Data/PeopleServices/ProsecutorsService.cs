﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheJudgesystem.Data.Common.Repositories;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Services.Mapping;
using TheJudgesystem.Web.ViewModels.Prosecutors;

namespace TheJudgesystem.Services.Data.PeopleServices
{
    public class ProsecutorsService : IProsecutorsService
    {
        private readonly IDeletableEntityRepository<Case> casesRepository;
        private readonly IDeletableEntityRepository<Prosecutor> prosecutorsRepository;
        private readonly IDeletableEntityRepository<Defendant> defendantsRepository;
        private readonly IUsersService usersService;

        public ProsecutorsService(
            IDeletableEntityRepository<Case> casesRepository,
            IDeletableEntityRepository<Prosecutor> prosecutorsRepository,
            IDeletableEntityRepository<Defendant> defendantsRepository,
            IUsersService usersService)
        {
            this.casesRepository = casesRepository;
            this.prosecutorsRepository = prosecutorsRepository;
            this.defendantsRepository = defendantsRepository;
            this.usersService = usersService;
        }


        public int GetCasesCount()
        {
            return this.casesRepository.AllAsNoTracking()
                .Where(x => x.LawyerDefence != null)
                .Count();
        }

        public Prosecutor GetProsecutor(ClaimsPrincipal user)
        {
            var userId = this.usersService.GetApplicationUserId(user);
            return this.prosecutorsRepository.All().FirstOrDefault(x => x.UserId == userId);
        }

        public IEnumerable<CaseInList> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4)
        {
            return this.casesRepository.All()
                .OrderByDescending(x => x.Id)
                .Where(x => x.LawyerDefence != null)
                .Where(x => x.IsSolved == false)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<CaseInList>()
                .ToList();
        }

        public async Task DecideForGuilty(DecisionInputModel input, int caseId)
        {
            var casee = this.casesRepository.All().FirstOrDefault(x => x.Id == caseId);
            var defendant = this.defendantsRepository.All().FirstOrDefault(x => x.CaseId == caseId);

            casee.ProsecutorDecision = input.ProsecutorDecision;
            defendant.IsGuilty = true;

            await this.defendantsRepository.SaveChangesAsync();
            await this.casesRepository.SaveChangesAsync();
        }

        public async Task DecideForNotGuilty(DecisionInputModel input, int caseId)
        {
            var casee = this.casesRepository.All().FirstOrDefault(x => x.Id == caseId);
            var defendant = this.defendantsRepository.All().FirstOrDefault(x => x.CaseId == caseId);

            casee.ProsecutorDecision = input.ProsecutorDecision;
            casee.IsSolved = true;

            await this.defendantsRepository.SaveChangesAsync();
            await this.casesRepository.SaveChangesAsync();
        }

        public async Task DecideForFee(DecisionInputModel input, int caseId)
        {
            // Add fee to defendant

            var casee = this.casesRepository.All().FirstOrDefault(x => x.Id == caseId);
            var defendant = this.defendantsRepository.All().FirstOrDefault(x => x.CaseId == caseId);

            casee.ProsecutorDecision = input.ProsecutorDecision;
            casee.IsSolved = true;

            await this.defendantsRepository.SaveChangesAsync();
            await this.casesRepository.SaveChangesAsync();
        }
    }
}