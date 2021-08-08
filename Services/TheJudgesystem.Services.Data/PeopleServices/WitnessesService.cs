using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
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


        public int GetCasesCount()
        {
            return this.casesRepository.AllAsNoTracking()
                .Where(x => x.LawyerDefence != null)
                .Where(x => x.ProsecutorDecision != null)
                .Where(x => x.IsSolved == false)
                .Count();
        }

        public Witness GetWitness(ClaimsPrincipal user)
        {
            var userId = this.usersService.GetApplicationUserId(user);
            return this.witnessesRepository.All().FirstOrDefault(x => x.UserId == userId);
        }

        public IEnumerable<CaseInList> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4)
        {
            return this.casesRepository.All()
                .OrderByDescending(x => x.Id)
                .Where(x => x.LawyerDefence != null)
                .Where(x => x.ProsecutorDecision != null)
                .Where(x => x.IsSolved == false)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<CaseInList>()
                .ToList();
        }

    }
}
