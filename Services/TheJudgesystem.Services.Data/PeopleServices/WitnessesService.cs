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
using TheJudgesystem.Web.ViewModels.Witnesses;

namespace TheJudgesystem.Services.Data.PeopleServices
{
    public class WitnessesService : IWitnessesService
    {
        private readonly IDeletableEntityRepository<Case> casesRepository;
        private readonly IDeletableEntityRepository<Witness> witnessesRepository;
        private readonly IUsersService usersService;

        public WitnessesService(
            IDeletableEntityRepository<Case> casesRepository,
            IDeletableEntityRepository<Witness> witnessesRepository,
            IUsersService usersService)
        {
            this.casesRepository = casesRepository;
            this.witnessesRepository = witnessesRepository;
            this.usersService = usersService;
        }

        public async Task<int> GetCasesCount()
        {
            var count = await this.casesRepository.AllAsNoTracking()
                .Where(x => string.IsNullOrWhiteSpace(x.LawyerDefence)
                        && string.IsNullOrWhiteSpace(x.ProsecutorDecision)
                        && !x.IsSolved)
                .CountAsync();
            return count;
        }

        public async Task<Witness> GetWitness(ClaimsPrincipal user)
        {
            var userId = this.usersService.GetApplicationUserId(user);
            var witness = await this.witnessesRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);
            return witness;
        }

        public async Task<ICollection<CaseInList>> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4)
        {
            var result = await this.casesRepository.All()
                .OrderByDescending(x => x.Id)
                .Where(x => string.IsNullOrWhiteSpace(x.LawyerDefence)
                        && string.IsNullOrWhiteSpace(x.ProsecutorDecision)
                        && !x.IsSolved)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<CaseInList>()
                .ToListAsync();

            return result;
        }
    }
}
