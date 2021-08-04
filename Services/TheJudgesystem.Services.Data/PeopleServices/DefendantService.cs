namespace TheJudgesystem.Services.Data.PeopleServices
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using TheJudgesystem.Data.Common.Repositories;
    using TheJudgesystem.Data.Models;
    using TheJudgesystem.Services.Mapping;
    using TheJudgesystem.Web.ViewModels.Defendants;

    public class DefendantService : IDefendantService
    {
        private readonly IUsersService usersService;
        private readonly IDeletableEntityRepository<Lawyer> lawyersRepository;
        private readonly IDeletableEntityRepository<Defendant> defendantsRepository;

        public DefendantService(
            IUsersService usersService,
            IDeletableEntityRepository<Lawyer> lawyersRepository,
            IDeletableEntityRepository<Defendant> defendantsRepository)
        {
            this.usersService = usersService;
            this.lawyersRepository = lawyersRepository;
            this.defendantsRepository = defendantsRepository;
        }

        public int GetCount()
        {
            return this.lawyersRepository.AllAsNoTracking().Count();
        }

        public Defendant GetDefendant(ClaimsPrincipal user)
        {
            var userId = this.usersService.GetApplicationUserId(user);
            return this.defendantsRepository.All().FirstOrDefault(x => x.UserId == userId);
        }

        public IEnumerable<LawyerInList> GetLawyers(int page, int itemsPerPage = 6)
        {
            return this.lawyersRepository.All()
                .OrderByDescending(x => x.Rating)
                .ThenByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<LawyerInList>()
                //.Select(x => new LawyerInList
                //{
                //    Id = x.Id,
                //    FirstName = x.FirstName,
                //    LastName = x.LastName,
                //    Rating = x.Rating,
                //    ImageUrl = x.ImageUrl,
                //    Salary = x.Salary,
                //    CV = new CvInList
                //    {
                //        Age = x.CV.Age,
                //        BirthTown = x.CV.BirthTown,
                //        School = x.CV.School,
                //        University = x.CV.University,
                //        Achievements = x.CV.Achievements,
                //    },
                //})
                .ToList();
        }

        public bool HasLawyer(ClaimsPrincipal user)
        {
            var defendant = this.GetDefendant(user);

            if (defendant.Lawyer != null)
            {
                return true;
            }

            return false;
        }

        public async Task HireLawyer(int id, ClaimsPrincipal user)
        {
            var defendant = this.GetDefendant(user);
            var lawyer = this.lawyersRepository.All().FirstOrDefault(x => x.Id == id);

            defendant.Lawyer = lawyer;
            defendant.LawyerId = lawyer.Id;
            lawyer.Clients.Add(defendant);

            await this.defendantsRepository.SaveChangesAsync();
            await this.lawyersRepository.SaveChangesAsync();
        }
    }
}
