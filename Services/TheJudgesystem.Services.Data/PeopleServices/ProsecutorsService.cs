using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<int> GetCasesCount()
        {
            var count = await this.casesRepository.AllAsNoTracking()
                .Where(x => !string.IsNullOrWhiteSpace(x.LawyerDefence)
                && string.IsNullOrWhiteSpace(x.ProsecutorDecision)
                        && !x.IsSolved)
                .CountAsync();
            return count;
        }

        public async Task<Prosecutor> GetProsecutor(ClaimsPrincipal user)
        {
            var userId = this.usersService.GetApplicationUserId(user);
            var prosecutor = await this.prosecutorsRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);
            return prosecutor;
        }

        public async Task<ICollection<CaseInList>> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4)
        {
            var result = await this.casesRepository.All()
                .OrderByDescending(x => x.Id)
                .Where(x => !string.IsNullOrWhiteSpace(x.LawyerDefence)
                        && string.IsNullOrWhiteSpace(x.ProsecutorDecision)
                        && !x.IsSolved)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<CaseInList>()
                .ToListAsync();

            return result;
        }

        public async Task DecideForGuilty(DecisionInputModel input, int caseId, ClaimsPrincipal user)
        {
            var @case = await this.casesRepository.All().FirstOrDefaultAsync(x => x.Id == caseId);
            var defendant = await this.defendantsRepository.All().FirstOrDefaultAsync(x => x.CaseId == caseId);

            var prosecutor = await this.GetProsecutor(user);

            @case.ProsecutorId = prosecutor.Id;
            @case.ProsecutorDecision = input.ProsecutorDecision;
            defendant.IsGuilty = true;

            await this.casesRepository.SaveChangesAsync();
        }

        public async Task DecideForNotGuilty(DecisionInputModel input, int caseId, ClaimsPrincipal user)
        {
            var @case = await this.casesRepository.All().FirstOrDefaultAsync(x => x.Id == caseId);

            var prosecutor = await this.GetProsecutor(user);

            @case.ProsecutorId = prosecutor.Id;
            @case.ProsecutorDecision = input.ProsecutorDecision;
            @case.IsSolved = true;

            await this.casesRepository.SaveChangesAsync();
        }

        public async Task DecideForFee(DecisionInputModel input, int caseId, ClaimsPrincipal user)
        {
            var @case = await this.casesRepository.All().FirstOrDefaultAsync(x => x.Id == caseId);

            var prosecutor = await this.GetProsecutor(user);
            var defendant = await this.defendantsRepository.All().FirstOrDefaultAsync(x => x.CaseId == caseId);

            @case.ProsecutorId = prosecutor.Id;
            @case.ProsecutorDecision = input.ProsecutorDecision;
            @case.IsSolved = true;
            defendant.HasFees = true;

            await this.casesRepository.SaveChangesAsync();
        }
    }
}
