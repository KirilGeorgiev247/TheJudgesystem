namespace TheJudgesystem.Services.Data
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using TheJudgesystem.Common;
    using TheJudgesystem.Data.Common.Repositories;
    using TheJudgesystem.Data.Models;
    using TheJudgesystem.Web.ViewModels.Roles;

    public class RolesService : IRolesService
    {
        private readonly IUsersService usersService;
        private readonly IDeletableEntityRepository<Lawyer> lawyersRepository;
        private readonly IDeletableEntityRepository<CV> cvsRepository;
        private readonly IDeletableEntityRepository<Judge> judgesRepository;
        private readonly IDeletableEntityRepository<Witness> witnessesRepository;
        private readonly IDeletableEntityRepository<Prosecutor> prosecutorsRepository;
        private readonly IDeletableEntityRepository<Defendant> defendantsRepository;
        private readonly IDeletableEntityRepository<Guard> guardsRepository;
        private readonly IDeletableEntityRepository<Jurymember> juryMembersRepository;

        public RolesService(
            IUsersService usersService,
            IDeletableEntityRepository<Lawyer> lawyersRepository,
            IDeletableEntityRepository<CV> cvsRepository,
            IDeletableEntityRepository<Judge> judgesRepository,
            IDeletableEntityRepository<Witness> witnessesRepository,
            IDeletableEntityRepository<Prosecutor> prosecutorsRepository,
            IDeletableEntityRepository<Defendant> defendantsRepository,
            IDeletableEntityRepository<Guard> guardsRepository,
            IDeletableEntityRepository<Jurymember> juryMembersRepository)
        {
            this.usersService = usersService;
            this.lawyersRepository = lawyersRepository ?? throw new ArgumentNullException(nameof(lawyersRepository));
            this.cvsRepository = cvsRepository ?? throw new ArgumentNullException(nameof(cvsRepository));
            this.judgesRepository = judgesRepository;
            this.witnessesRepository = witnessesRepository;
            this.prosecutorsRepository = prosecutorsRepository;
            this.defendantsRepository = defendantsRepository;
            this.guardsRepository = guardsRepository;
            this.juryMembersRepository = juryMembersRepository;
        }

        public async Task AddLawyer(LawyerInputModel lawyer, ClaimsPrincipal user)
        {
            var realCV = new CV
            {
                FirstName = lawyer.FirstName,
                LastName = lawyer.LastName,
                Age = lawyer.Age,
                ImageUrl = lawyer.ImageUrl,
                BirthTown = lawyer.BirthTown,
                School = lawyer.School,
                University = lawyer.University,
                Achievements = lawyer.Achievements,
            };

            //await this.cvsRepository.AddAsync(realCV);
            //await this.cvsRepository.SaveChangesAsync();

            var realLawyer = new Lawyer
            {
                UserId = this.usersService.GetApplicationUserId(user),
                FirstName = lawyer.FirstName,
                LastName = lawyer.LastName,
                ImageUrl = lawyer.ImageUrl,
                Salary = lawyer.Salary,
                CV = realCV,
            };

            await this.usersService.SetRole(GlobalConstants.LawyerRole, user);

            await this.lawyersRepository.AddAsync(realLawyer);
            await this.lawyersRepository.SaveChangesAsync();

        }

        public async Task AddJudge(JudgeInputModel judge, ClaimsPrincipal user)
        {
            var realCV = new CV
            {
                FirstName = judge.FirstName,
                LastName = judge.LastName,
                Age = judge.Age,
                ImageUrl = judge.ImageUrl,
                BirthTown = judge.BirthTown,
                School = judge.School,
                University = judge.University,
                Achievements = judge.Achievements,
            };

            //await this.cvsRepository.AddAsync(realCV);
            //await this.cvsRepository.SaveChangesAsync();

            var realJudge = new Judge
            {
                UserId = this.usersService.GetApplicationUserId(user),
                FirstName = judge.FirstName,
                LastName = judge.LastName,
                ImageUrl = judge.ImageUrl,
                Salary = judge.Salary,
                CV = realCV,
            };

            await this.usersService.SetRole(GlobalConstants.JudgeRole, user);

            await this.judgesRepository.AddAsync(realJudge);
            await this.judgesRepository.SaveChangesAsync();

        }

        public async Task AddWitness(WitnessInputModel witness, ClaimsPrincipal user)
        {
            var realWitness = new Witness
            {
                UserId = this.usersService.GetApplicationUserId(user),
                FirstName = witness.FirstName,
                LastName = witness.LastName,
                ImageUrl = witness.ImageUrl,
            };

            await this.usersService.SetRole(GlobalConstants.WitnessRole, user);

            await this.witnessesRepository.AddAsync(realWitness);
            await this.witnessesRepository.SaveChangesAsync();

        }

        public async Task AddProsecutor(ProsecutorInputModel prosecutor, ClaimsPrincipal user)
        {
            var realProsecutor = new Prosecutor
            {
                UserId = this.usersService.GetApplicationUserId(user),
                FirstName = prosecutor.FirstName,
                LastName = prosecutor.LastName,
                ImageUrl = prosecutor.ImageUrl,
            };

            await this.usersService.SetRole(GlobalConstants.ProsecutorRole, user);

            await this.prosecutorsRepository.AddAsync(realProsecutor);
            await this.prosecutorsRepository.SaveChangesAsync();

        }

        public async Task AddDefendant(DefendantInputModel defendant, ClaimsPrincipal user)
        {
            var realDefendant = new Defendant
            {
                UserId = this.usersService.GetApplicationUserId(user),
                FirstName = defendant.FirstName,
                LastName = defendant.LastName,
                ImageUrl = defendant.ImageUrl,
                Job = defendant.Job,
                Charges = defendant.Charges,
                IsGuilty = false,
            };

            await this.usersService.SetRole(GlobalConstants.DefendantRole, user);

            await this.defendantsRepository.AddAsync(realDefendant);
            await this.defendantsRepository.SaveChangesAsync();

        }

        public async Task AddGuard(GuardInputModel guard, ClaimsPrincipal user)
        {
            var realGuard = new Guard
            {
                UserId = this.usersService.GetApplicationUserId(user),
                FirstName = guard.FirstName,
                LastName = guard.LastName,
                ImageUrl = guard.ImageUrl,
                Salary = guard.Salary,
            };

            await this.usersService.SetRole(GlobalConstants.GuardRole, user);

            await this.guardsRepository.AddAsync(realGuard);
            await this.guardsRepository.SaveChangesAsync();

        }

        public async Task AddJuryMember(JuryMemberInputModel juryMember, ClaimsPrincipal user)
        {
            var realJuryMember = new Jurymember
            {
                UserId = this.usersService.GetApplicationUserId(user),
                FirstName = juryMember.FirstName,
                LastName = juryMember.LastName,
            };

            await this.usersService.SetRole(GlobalConstants.JurymemberRole, user);

            await this.juryMembersRepository.AddAsync(realJuryMember);
            await this.juryMembersRepository.SaveChangesAsync();
        }
    }
}
