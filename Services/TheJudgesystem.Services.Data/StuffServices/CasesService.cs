using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheJudgesystem.Data.Common.Repositories;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Services.Data.PeopleServices;
using TheJudgesystem.Web.ViewModels.Cases;

namespace TheJudgesystem.Services.Data.StuffServices
{
    public class CasesService : ICasesService
    {
        private readonly IDeletableEntityRepository<Lawyer> lawyersRepository;
        private readonly IDeletableEntityRepository<Defendant> defendantsRepository;
        private readonly IDeletableEntityRepository<Case> casesRepository;
        private readonly IUsersService usersService;
        private readonly IDefendantService defendantService;

        public CasesService(
            IDeletableEntityRepository<Lawyer> lawyersRepository,
            IDeletableEntityRepository<Defendant> defendantsRepository,
            IDeletableEntityRepository<Case> casesRepository,
            IUsersService usersService,
            IDefendantService defendantService)
        {
            this.lawyersRepository = lawyersRepository;
            this.defendantsRepository = defendantsRepository;
            this.casesRepository = casesRepository;
            this.usersService = usersService;
            this.defendantService = defendantService;
        }

        public async Task AddCase(CaseInputModel input, ClaimsPrincipal user, int id)
        {
            var defendant = await this.defendantService.GetDefendant(user);
            var lawyer = await this.lawyersRepository.All().FirstOrDefaultAsync(x => x.Id == input.LawyerId);
            defendant.LawyerId = lawyer.Id;

            var realCase = new Case
            {
                DefendantId = defendant.Id,
                Description = input.Description,
                LawyerId = lawyer.Id,
            };

            await this.casesRepository.AddAsync(realCase);

            await this.casesRepository.SaveChangesAsync();

            defendant.CaseId = realCase.Id;

            await this.casesRepository.SaveChangesAsync();
        }
    }
}
