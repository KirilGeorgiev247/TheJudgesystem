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
using TheJudgesystem.Web.ViewModels.JuryMembers;

namespace TheJudgesystem.Services.Data.PeopleServices
{
    public class JurymembersService : IJurymembersService
    {

        private readonly IDeletableEntityRepository<Case> casesRepository;
        private readonly IDeletableEntityRepository<Jurymember> juryMembersRepository;
        private readonly IUsersService usersService;

        public JurymembersService(
            IDeletableEntityRepository<Case> casesRepository,
            IDeletableEntityRepository<Jurymember> juryMembersRepository,
            IUsersService usersService)
        {
            this.casesRepository = casesRepository;
            this.juryMembersRepository = juryMembersRepository;
            this.usersService = usersService;
        }

        public async Task<int> GetCasesCount(ClaimsPrincipal user)
        {
            var juryMember = await this.GetJuryMember(user);

            var count = await this.casesRepository.AllAsNoTracking()
                .Where(x => !string.IsNullOrWhiteSpace(x.LawyerDefence)
                        && !string.IsNullOrWhiteSpace(x.ProsecutorDecision)
                        && !x.IsSolved
                        && x.Defendant.IsGuilty
                        && x.Indications.Count != 0
                        && !x.Jury.Members.Any(x => x.Id == juryMember.Id)
                        && x.Jury.Members.Count < 3)
                .CountAsync();
            return count;
        }

        public async Task<Jurymember> GetJuryMember(ClaimsPrincipal user)
        {
            var userId = this.usersService.GetApplicationUserId(user);
            var juryMember = await this.juryMembersRepository.All().FirstOrDefaultAsync(x => x.UserId == userId);
            return juryMember;
        }

        public async Task<ICollection<CaseInList>> GetCases(ClaimsPrincipal user, int page, int itemsPerPage = 4)
        {
            var juryMember = await this.GetJuryMember(user);

            var result = await this.casesRepository.All()
                .OrderByDescending(x => x.Id)
                .Where(x => !string.IsNullOrWhiteSpace(x.LawyerDefence)
                        && !string.IsNullOrWhiteSpace(x.ProsecutorDecision)
                        && !x.IsSolved
                        && x.Defendant.IsGuilty
                        && x.Indications.Count != 0
                        && !x.Jury.Members.Any(x => x.Id == juryMember.Id)
                        && x.Jury.Members.Count < 3)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<CaseInList>()
                .ToListAsync();

            return result;
        }
    }
}
