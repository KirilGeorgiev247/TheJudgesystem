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

        public JudgesService(
            IDeletableEntityRepository<Judge> judgesRepository,
            IDeletableEntityRepository<Case> casesRepository,
            IUsersService usersService,
            IDeletableEntityRepository<Jury> juriesRepository)
        {
            this.judgesRepository = judgesRepository;
            this.casesRepository = casesRepository;
            this.usersService = usersService;
            this.juriesRepository = juriesRepository;
        }

        public async Task<int> GetCasesCount()
        {
            var count = await this.casesRepository.AllAsNoTracking()
                .Where(x => string.IsNullOrWhiteSpace(x.Jury.Pronouncement)
                        && !x.IsSolved
                        && x.Defendant.IsGuilty)
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
                .Where(x => string.IsNullOrWhiteSpace(x.Jury.Pronouncement)
                        && !x.IsSolved
                        && x.Defendant.IsGuilty)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<CaseInList>()
                .ToListAsync();

            return result;
        }

    }
}
