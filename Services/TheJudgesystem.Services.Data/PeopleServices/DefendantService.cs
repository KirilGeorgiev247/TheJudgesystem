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
        private readonly IDeletableEntityRepository<Lawyer> lawyersRepository;
        private readonly IDeletableEntityRepository<Defendant> defendantsRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public DefendantService(
            IDeletableEntityRepository<Lawyer> lawyersRepository,
            IDeletableEntityRepository<Defendant> defendantsRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.lawyersRepository = lawyersRepository;
            this.defendantsRepository = defendantsRepository;
            this.userManager = userManager;
        }

        public int GetCount()
        {
            return this.lawyersRepository.AllAsNoTracking().Count();
        }

        public IEnumerable<LawyerInList> GetLawyers(int page, int itemsPerPage = 6)
        {
            return this.lawyersRepository.AllAsNoTracking()
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

        public async Task HireLawyer(int id, ClaimsPrincipal user)
        {
            var defendant = this.defendantsRepository.All().FirstOrDefault(x => x.UserId == this.GetUser(user).Id);
            var lawyer = this.lawyersRepository.All().FirstOrDefault(x => x.Id == id);

            defendant.LawyerId = id;
            lawyer.Clients.Add(defendant);

            await this.defendantsRepository.SaveChangesAsync();
            await this.lawyersRepository.SaveChangesAsync();
        }

        private async Task<ApplicationUser> GetUser(ClaimsPrincipal user)
        {
            var currentUser = await this.userManager.GetUserAsync(user);

            return currentUser;
        }
    }
}
