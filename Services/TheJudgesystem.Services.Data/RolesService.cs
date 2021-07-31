namespace TheJudgesystem.Services.Data
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using TheJudgesystem.Data.Common.Repositories;
    using TheJudgesystem.Data.Models;
    using TheJudgesystem.Web.ViewModels.Roles;

    public class RolesService : IRolesService
    {
        private readonly IDeletableEntityRepository<Lawyer> lawyersRepository;
        private readonly IDeletableEntityRepository<CV> cvsRepository;
        private readonly IDeletableEntityRepository<Judge> judgesRepository;
        private readonly IDeletableEntityRepository<Witness> witnessesRepository;
        private readonly IDeletableEntityRepository<Prosecutor> prosecutorsRepository;
        private readonly IDeletableEntityRepository<Defendant> defendantsRepository;
        private readonly IDeletableEntityRepository<Guard> guardsRepository;
        private readonly IDeletableEntityRepository<JuryMember> juryMembersRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public RolesService(
            IDeletableEntityRepository<Lawyer> lawyersRepository,
            IDeletableEntityRepository<CV> cvsRepository,
            IDeletableEntityRepository<Judge> judgesRepository,
            IDeletableEntityRepository<Witness> witnessesRepository,
            IDeletableEntityRepository<Prosecutor> prosecutorsRepository,
            IDeletableEntityRepository<Defendant> defendantsRepository,
            IDeletableEntityRepository<Guard> guardsRepository,
            IDeletableEntityRepository<JuryMember> juryMembersRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.lawyersRepository = lawyersRepository ?? throw new ArgumentNullException(nameof(lawyersRepository));
            this.cvsRepository = cvsRepository ?? throw new ArgumentNullException(nameof(cvsRepository));
            this.judgesRepository = judgesRepository;
            this.witnessesRepository = witnessesRepository;
            this.prosecutorsRepository = prosecutorsRepository;
            this.defendantsRepository = defendantsRepository;
            this.guardsRepository = guardsRepository;
            this.juryMembersRepository = juryMembersRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
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
            };

            if (string.IsNullOrEmpty(lawyer.Achievements))
            {
                realCV.Achievements = "Not given!";
            }
            else
            {
                realCV.Achievements = lawyer.Achievements;
            }

            await this.cvsRepository.AddAsync(realCV);
            await this.cvsRepository.SaveChangesAsync();

            var realLawyer = new Lawyer
            {
                FirstName = lawyer.FirstName,
                LastName = lawyer.LastName,
                ImageUrl = lawyer.ImageUrl,
                Salary = lawyer.Salary,
                CV = realCV,
            };

            await this.lawyersRepository.AddAsync(realLawyer);
            await this.lawyersRepository.SaveChangesAsync();

            await this.GetRole("Lawyer", user);
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
            };

            if (judge.Achievements.Length < 5)
            {
                realCV.Achievements = "Not given!";
            }
            else
            {
                realCV.Achievements = judge.Achievements;
            }

            await this.cvsRepository.AddAsync(realCV);
            await this.cvsRepository.SaveChangesAsync();

            var realJudge = new Judge
            {
                FirstName = judge.FirstName,
                LastName = judge.LastName,
                ImageUrl = judge.ImageUrl,
                Salary = judge.Salary,
                CV = realCV,
            };

            await this.judgesRepository.AddAsync(realJudge);
            await this.judgesRepository.SaveChangesAsync();

            await this.GetRole("Judge", user);
        }

        public async Task AddWitness(WitnessInputModel witness, ClaimsPrincipal user)
        {
            var realWitness = new Witness
            {
                FirstName = witness.FirstName,
                LastName = witness.LastName,
                ImageUrl = witness.ImageUrl,
            };

            await this.witnessesRepository.AddAsync(realWitness);
            await this.witnessesRepository.SaveChangesAsync();

            await this.GetRole("Witness", user);
        }

        public async Task AddProsecutor(ProsecutorInputModel prosecutor, ClaimsPrincipal user)
        {
            var realProsecutor = new Prosecutor
            {
                FirstName = prosecutor.FirstName,
                LastName = prosecutor.LastName,
                ImageUrl = prosecutor.ImageUrl,
            };

            await this.prosecutorsRepository.AddAsync(realProsecutor);
            await this.prosecutorsRepository.SaveChangesAsync();

            await this.GetRole("Prosecutor", user);
        }

        public async Task AddDefendant(DefendantInputModel defendant, ClaimsPrincipal user)
        {
            var realDefendant = new Defendant
            {
                FirstName = defendant.FirstName,
                LastName = defendant.LastName,
                ImageUrl = defendant.ImageUrl,
                Job = defendant.Job,
                Charges = defendant.Charges,
                IsGuilty = false,
            };

            await this.defendantsRepository.AddAsync(realDefendant);
            await this.defendantsRepository.SaveChangesAsync();

            await this.GetRole("Defendant", user);
        }

        public async Task AddGuard(GuardInputModel guard, ClaimsPrincipal user)
        {
            var realGuard = new Guard
            {
                FirstName = guard.FirstName,
                LastName = guard.LastName,
                ImageUrl = guard.ImageUrl,
                Salary = guard.Salary,
            };

            await this.guardsRepository.AddAsync(realGuard);
            await this.guardsRepository.SaveChangesAsync();

            await this.GetRole("Guard", user);
        }

        public async Task AddJuryMember(JuryMemberInputModel juryMember, ClaimsPrincipal user)
        {
            var realJuryMember = new JuryMember
            {
                FirstName = juryMember.FirstName,
                LastName = juryMember.LastName,
            };

            await this.juryMembersRepository.AddAsync(realJuryMember);
            await this.juryMembersRepository.SaveChangesAsync();

            await this.GetRole("JuryMember", user);
        }

        private async Task GetRole(string role, ClaimsPrincipal user)
        {
            if (this.roleManager.Roles.Any(x => x.Name == role))
            {
                var currentUser = await this.userManager.GetUserAsync(user);
                await this.userManager.AddToRoleAsync(currentUser, role);
            }
            else
            {
                await this.roleManager.CreateAsync(new ApplicationRole
                {
                    Name = role,
                });

                var currentUser = await this.userManager.GetUserAsync(user);
                await this.userManager.AddToRoleAsync(currentUser, role);
            }
        }
    }
}
