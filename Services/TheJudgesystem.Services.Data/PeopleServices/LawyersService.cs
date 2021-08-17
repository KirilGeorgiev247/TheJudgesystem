using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TheJudgesystem.Data.Common.Repositories;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Web.ViewModels.Lawyers;
using TheJudgesystem.Services.Mapping;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TheJudgesystem.Services.Data.PeopleServices
{
    public class LawyersService : ILawyersService
    {
        private readonly IDeletableEntityRepository<Case> casesRepository;
        private readonly IDeletableEntityRepository<Lawyer> lawyersRepository;
        private readonly IDeletableEntityRepository<Defendant> defendantsRepository;
        private readonly IUsersService usersService;

        public LawyersService(
            IDeletableEntityRepository<Case> casesRepository,
            IDeletableEntityRepository<Lawyer> lawyersRepository,
            IDeletableEntityRepository<Defendant> defendantsRepository,
            IUsersService usersService)
        {
            this.casesRepository = casesRepository;
            this.lawyersRepository = lawyersRepository;
            this.defendantsRepository = defendantsRepository;
            this.usersService = usersService;
        }

        public async Task<int> GetCasesCount()
        {
            var count = await this.casesRepository.AllAsNoTracking()
                .Where(x => string.IsNullOrWhiteSpace(x.LawyerDefence) 
                        && !x.IsSolved)
                .CountAsync();
            return count;
        }

        public async Task<Lawyer> GetLawyer(ClaimsPrincipal user)
        {
            var userId = this.usersService.GetApplicationUserId(user);
            var lawyer = await this.lawyersRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);
            return lawyer;
        }

        public async Task<ICollection<CaseInList>> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4)
        {
            var lawyer = await this.GetLawyer(user);

            var result = await this.casesRepository.All()
                .OrderByDescending(x => x.Id)
                .Where(x => x.LawyerId == lawyer.Id
                        && string.IsNullOrWhiteSpace(x.LawyerDefence)
                        && !x.IsSolved)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<CaseInList>()
                .ToListAsync();

            return result;
        }

        public async Task AddDefence(DefenceInputModel input, int caseId)
        {
            var @case = await this.casesRepository.All().FirstOrDefaultAsync(x => x.Id == caseId);

            @case.LawyerDefence = input.LawyerDefence;

            await this.casesRepository.SaveChangesAsync();
        }
    }
}
