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

        public int GetCasesCount()
        {
            return this.casesRepository.AllAsNoTracking()
                .Where(x => x.LawyerDefence == null)
                .Count();
        }

        public Lawyer GetLawyer(ClaimsPrincipal user)
        {
            var userId = this.usersService.GetApplicationUserId(user);
            return this.lawyersRepository.All().FirstOrDefault(x => x.UserId == userId);
        }

        public IEnumerable<CaseInList> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4)
        {
            return this.casesRepository.All()
                .OrderByDescending(x => x.Id)
                .Where(x => x.LawyerId == this.GetLawyer(user).Id)
                .Where(x => x.LawyerDefence == null)
                .Where(x => x.IsSolved == false)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<CaseInList>()
                .ToList();
        }

        public async Task AddDefence(DefenceInputModel input, int caseId)
        {

            var casee = this.casesRepository.All().FirstOrDefault(x => x.Id == caseId);

            casee.LawyerDefence = input.LawyerDefence;

            await this.casesRepository.SaveChangesAsync();

        }
    }
}
