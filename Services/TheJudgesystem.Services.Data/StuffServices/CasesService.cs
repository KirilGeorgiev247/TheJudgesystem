using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheJudgesystem.Data.Common.Repositories;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Web.ViewModels.Cases;

namespace TheJudgesystem.Services.Data.StuffServices
{
    public class CasesService : ICasesService
    {
        private readonly IDeletableEntityRepository<Lawyer> lawyersRepository;
        private readonly IDeletableEntityRepository<Defendant> defendantsRepository;
        private readonly IDeletableEntityRepository<Case> casesRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CasesService(
            IDeletableEntityRepository<Lawyer> lawyersRepository,
            IDeletableEntityRepository<Defendant> defendantsRepository,
            IDeletableEntityRepository<Case> casesRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.lawyersRepository = lawyersRepository;
            this.defendantsRepository = defendantsRepository;
            this.casesRepository = casesRepository;
            this.userManager = userManager;
        }

        public async Task AddCase(CaseInputModel input, ClaimsPrincipal user)
        {
            var defendant = this.defendantsRepository.All().FirstOrDefault(x => x.UserId == this.GetUser(user).Id);
            var lawyer = this.lawyersRepository.All().FirstOrDefault(x => x.Id == input.LawyerId);

            var realCase = new Case
            {
                DefendantId = defendant.Id,
                Description = input.Description,
                LawyerId = lawyer.Id,
            };

            await this.casesRepository.AddAsync(realCase);
            await this.casesRepository.SaveChangesAsync();
        }

        private async Task<ApplicationUser> GetUser(ClaimsPrincipal user)
        {
            var currentUser = await this.userManager.GetUserAsync(user);

            return currentUser;
        }
    }
}
