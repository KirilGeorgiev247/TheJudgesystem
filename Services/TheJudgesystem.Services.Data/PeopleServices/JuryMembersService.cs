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
using TheJudgesystem.Web.ViewModels.Jurymembers;
using TheJudgesystem.Web.ViewModels.Jurymember;
using TheJudgesystem.Data.Common.Enumerations;
using TheJudgesystem.Common;

namespace TheJudgesystem.Services.Data.PeopleServices
{
    public class JurymembersService : IJurymembersService
    {

        private readonly IDeletableEntityRepository<Case> casesRepository;
        private readonly IDeletableEntityRepository<Jurymember> juryMembersRepository;
        private readonly IDeletableEntityRepository<Opinion> opinionsRepository;
        private readonly IDeletableEntityRepository<Jury> juriesRepository;
        private readonly IUsersService usersService;

        public JurymembersService(
            IDeletableEntityRepository<Case> casesRepository,
            IDeletableEntityRepository<Jurymember> juryMembersRepository,
            IDeletableEntityRepository<Opinion> opinionsRepository,
            IDeletableEntityRepository<Jury> juriesRepository,
            IUsersService usersService)
        {
            this.casesRepository = casesRepository;
            this.juryMembersRepository = juryMembersRepository;
            this.opinionsRepository = opinionsRepository;
            this.juriesRepository = juriesRepository;
            this.usersService = usersService;
        }

        public async Task<int> GetCasesCount(ClaimsPrincipal user)
        {
            var juryMember = await this.GetJuryMember(user);
            var jury = await this.juriesRepository.All().FirstOrDefaultAsync(x => x.Id == juryMember.JuryId);

            bool hasAnnounced = false;
            bool hasThreeMembers = false;

            if (jury != null)
            {
                hasAnnounced = jury.Members.Any(x => x.Id == juryMember.Id);
                hasThreeMembers = jury.Members.Count == 3;
            }

            var count = await this.casesRepository.AllAsNoTracking()
                .Where(x => !string.IsNullOrWhiteSpace(x.LawyerDefence)
                        && !string.IsNullOrWhiteSpace(x.ProsecutorDecision)
                        && !x.IsSolved
                        && x.Defendant.IsGuilty
                        && x.Indications.Count != 0
                        && !hasAnnounced
                        && !hasThreeMembers)
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
            var jury = await this.juriesRepository.All().FirstOrDefaultAsync(x => x.Id == juryMember.JuryId);

            bool hasAnnounced = false;
            bool hasThreeMembers = false;

            if (jury != null)
            {
                hasAnnounced = jury.Members.Any(x => x.Id == juryMember.Id);
                hasThreeMembers = jury.Members.Count == 3;
            }

            var result = await this.casesRepository.All()
                .OrderByDescending(x => x.Id)
                .Where(x => !string.IsNullOrWhiteSpace(x.LawyerDefence)
                        && !string.IsNullOrWhiteSpace(x.ProsecutorDecision)
                        && !x.IsSolved
                        && x.Defendant.IsGuilty
                        && x.Indications.Count != 0
                        && !hasAnnounced
                        && !hasThreeMembers)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<CaseInList>()
                .ToListAsync();

            return result;
        }

        public async Task AddOpinion(OpinionInputModel input, int caseId, ClaimsPrincipal user)
        {
            var @case = await this.casesRepository.All().FirstOrDefaultAsync(x => x.Id == caseId);
            var juryMember = await this.GetJuryMember(user);

            if (@case.JuryId == null)
            {
                await this.CreateJury(caseId);
            }

            var jury = await this.juriesRepository.All().FirstOrDefaultAsync(x => x.CaseId == caseId);

            if (@case.JuryId == null)
            {
                @case.JuryId = jury.Id;
            }

            await this.CreateOpinion(input.Opinion, input.Description, juryMember.Id, @case.Id, jury.Id);

            var opinion = await this.opinionsRepository.All().FirstOrDefaultAsync(x => x.JurymemberId == juryMember.Id);

            juryMember.OpinionId = opinion.Id;

            jury.Members.Add(juryMember);
            juryMember.JuryId = jury.Id;
            jury.Opinions.Add(opinion);

            var count = jury.Members.Count;
            var opinionsCount = jury.Opinions.Count;

            if (jury.Members.Count == 2)
            {
                var guilty = 0;
                var notGuilty = 0;

                foreach (var member in jury.Members)
                {
                    switch (member.Opinion.Guiltiness)
                    {
                        case GuiltinessEnumeration.Guilty:
                            guilty++;
                            break;
                        case GuiltinessEnumeration.NotGuilty:
                            notGuilty++;
                            break;
                        default:
                            break;
                    }
                }

                if (guilty < notGuilty)
                {
                    jury.Pronouncement = GlobalConstants.JuryNotGuiltyPronouncement;
                }
                else
                {
                    jury.Pronouncement = GlobalConstants.JuryGuiltyPronouncement;
                }
            }

            await this.casesRepository.SaveChangesAsync();
        }

        private async Task CreateOpinion(GuiltinessEnumeration opinion, string description, int id1, int id2, int id3)
        {
            var realOpinion = new Opinion
            {
                Guiltiness = opinion,
                Description = description,
                JurymemberId = id1,
                CaseId = id2,
                JuryId = id3,
            };

            await this.opinionsRepository.AddAsync(realOpinion);
            await this.opinionsRepository.SaveChangesAsync();

        }

        private async Task CreateJury(int caseId)
        {
            var realJury = new Jury
            {
                CaseId = caseId,
            };

            await this.juriesRepository.AddAsync(realJury);
            await this.juriesRepository.SaveChangesAsync();
        }
    }
}
