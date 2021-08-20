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
using TheJudgesystem.Web.ViewModels.Judges;

namespace TheJudgesystem.Services.Data.PeopleServices
{
    public class JudgesService : IJudgesService
    {
        private readonly IDeletableEntityRepository<Judge> judgesRepository;
        private readonly IDeletableEntityRepository<Case> casesRepository;
        private readonly IUsersService usersService;
        private readonly IDeletableEntityRepository<Jury> juriesRepository;
        private readonly IDeletableEntityRepository<Defendant> defendantsRepository;

        public JudgesService(
            IDeletableEntityRepository<Judge> judgesRepository,
            IDeletableEntityRepository<Case> casesRepository,
            IDeletableEntityRepository<Jury> juriesRepository,
            IDeletableEntityRepository<Defendant> defendantsRepository,
            IUsersService usersService)
        {
            this.judgesRepository = judgesRepository;
            this.casesRepository = casesRepository;
            this.usersService = usersService;
            this.juriesRepository = juriesRepository;
            this.defendantsRepository = defendantsRepository;
        }

        public async Task<int> GetCasesCount()
        {
            var count = await this.casesRepository.AllAsNoTracking()
                .Where(x => !string.IsNullOrWhiteSpace(x.Jury.Pronouncement)
                        && string.IsNullOrWhiteSpace(x.JudgeDecision)
                        && !x.IsSolved
                        && x.Defendant.IsGuilty
                        && x.Indications.Count != 0)
                .CountAsync();
            return count;
        }

        public async Task<Judge> GetJudge(ClaimsPrincipal user)
        {
            var userId = this.usersService.GetApplicationUserId(user);
            var judge = await this.judgesRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);
            return judge;
        }

        public async Task<ICollection<CaseInList>> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4)
        {
            var result = await this.casesRepository.All()
                .OrderByDescending(x => x.Id)
                .Where(x => !string.IsNullOrWhiteSpace(x.Jury.Pronouncement)
                        && string.IsNullOrWhiteSpace(x.JudgeDecision)
                        && !x.IsSolved
                        && x.Defendant.IsGuilty
                        && x.Indications.Count != 0)
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

            @case.IsSolved = true;

            var judge = await this.GetJudge(user);

            @case.JudgeDecision = input.JudgeDecision;
            @case.JudgeId = judge.Id;

            defendant.IsGuilty = true;

            await this.casesRepository.SaveChangesAsync();
        }

        public async Task DecideForNotGuilty(DecisionInputModel input, int caseId, ClaimsPrincipal user)
        {
            var @case = await this.casesRepository.All().FirstOrDefaultAsync(x => x.Id == caseId);

            var judge = await this.GetJudge(user);

            @case.JudgeId = judge.Id;
            @case.JudgeDecision = input.JudgeDecision;
            @case.IsSolved = true;

            await this.casesRepository.SaveChangesAsync();
        }

        public async Task DecideForFee(DecisionInputModel input, int caseId, ClaimsPrincipal user)
        {
            var @case = await this.casesRepository.All().FirstOrDefaultAsync(x => x.Id == caseId);

            var judge = await this.GetJudge(user);
            var defendant = await this.defendantsRepository.All().FirstOrDefaultAsync(x => x.CaseId == caseId);

            @case.JudgeId = judge.Id;
            @case.JudgeDecision = input.JudgeDecision;
            @case.IsSolved = true;
            defendant.HasFees = true;

            await this.casesRepository.SaveChangesAsync();
        }

    }
}
