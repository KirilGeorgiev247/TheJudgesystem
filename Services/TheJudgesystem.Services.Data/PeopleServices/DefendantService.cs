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
        private readonly IDeletableEntityRepository<Case> casesRepository;

        public DefendantService(
            IUsersService usersService,
            IDeletableEntityRepository<Lawyer> lawyersRepository,
            IDeletableEntityRepository<Defendant> defendantsRepository,
            IDeletableEntityRepository<Case> casesRepository)
        {
            this.usersService = usersService;
            this.lawyersRepository = lawyersRepository;
            this.defendantsRepository = defendantsRepository;
            this.casesRepository = casesRepository;
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

        public InfoViewModel GetInfo(ClaimsPrincipal user)
        {
            var info = new InfoViewModel
            {
                Lawyer = this.GetMyLawyer(user),
                Case = this.GetMyCase(user),
                ImageUrl = this.GetMyImage(user).ImageUrl,
            };

            return info;
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

        public MyCaseViewModel GetMyCase(ClaimsPrincipal user)
        {
            return this.casesRepository.All()
                .Where(x => x.Id == this.GetDefendant(user).CaseId)
                .To<MyCaseViewModel>()
                .FirstOrDefault();
        }

        public MyLawyerViewModel GetMyLawyer(ClaimsPrincipal user)
        {
            return this.lawyersRepository.All()
                .Where(x => x.Id == this.GetDefendant(user).LawyerId)
                .To<MyLawyerViewModel>()
                .FirstOrDefault();
        }

        public MyImageViewModel GetMyImage(ClaimsPrincipal user)
        {
            var userId = this.usersService.GetApplicationUserId(user);
            return this.defendantsRepository.All()
                    .Where(x => x.UserId == userId)
                    .To<MyImageViewModel>()
                    .FirstOrDefault();
        }

        public bool HasLawyer(ClaimsPrincipal user)
        {
            var defendant = this.GetDefendant(user);

            if (defendant.LawyerId != null)
            {
                return true;
            }

            return false;
        }

        public async Task HireLawyer(int id, ClaimsPrincipal user)
        {
            var defendant = this.GetDefendant(user);
            var lawyer = this.lawyersRepository.All().FirstOrDefault(x => x.Id == id);

            defendant.LawyerId = lawyer.Id;

            await this.defendantsRepository.SaveChangesAsync();
        }
    }
}
