namespace TheJudgesystem.Services.Data.PeopleServices
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
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

        public async Task<int> GetCount()
        {
            var count = await this.lawyersRepository.AllAsNoTracking().CountAsync();

            return count;
        }

        public async Task<Defendant> GetDefendant(ClaimsPrincipal user)
        {
            var userId = this.usersService.GetApplicationUserId(user);
            var defendant = await this.defendantsRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);
            return defendant;
        }

        public async Task<InfoViewModel> GetInfo<T>(ClaimsPrincipal user)
        {
            var info = new InfoViewModel
            {
                Lawyer = await this.GetMyLawyer<MyLawyerViewModel>(user),
                Case = await this.GetMyCase<MyCaseViewModel>(user),
                ImageUrl = this.GetMyImage<MyImageViewModel>(user).Result.ImageUrl,
            };

            return info;
        }

        public async Task<ICollection<T>> GetLawyers<T>(int page, int itemsPerPage = 4)
        {
            var result = await this.lawyersRepository.All()
                .OrderByDescending(x => x.Rating)
                .ThenByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToListAsync();
            return result;
        }

        public async Task<T> GetMyCase<T>(ClaimsPrincipal user)
        {
            var defendant = await this.GetDefendant(user);

            var @case = await this.casesRepository.All()
                .Where(x => x.DefendantId == defendant.Id)
                .To<T>()
                .FirstOrDefaultAsync();

            return @case;
        }

        public async Task<T> GetMyLawyer<T>(ClaimsPrincipal user)
        {
            var defendant = await this.GetDefendant(user);

            var lawyer = await this.lawyersRepository.All()
                .Where(x => x.Id == defendant.LawyerId)
                .To<T>()
                .FirstOrDefaultAsync();
            return lawyer;
        }

        public async Task<T> GetMyImage<T>(ClaimsPrincipal user)
        {
            var userId = this.usersService.GetApplicationUserId(user);
            var image = await this.defendantsRepository.All()
                    .Where(x => x.UserId == userId)
                    .To<T>()
                    .FirstOrDefaultAsync();
            return image;
        }

        public async Task<bool> HasLawyer(ClaimsPrincipal user)
        {
            var defendant = await this.GetDefendant(user);

            if (defendant.LawyerId != null)
            {
                return true;
            }

            return false;
        }

        public async Task HireLawyer(int id, ClaimsPrincipal user)
        {
            var defendant = await this.GetDefendant(user);
            var lawyer = await this.lawyersRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            defendant.LawyerId = lawyer.Id;

            await this.defendantsRepository.SaveChangesAsync();
        }
    }
}
