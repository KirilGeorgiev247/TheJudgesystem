namespace TheJudgesystem.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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

        public RolesService(
            IDeletableEntityRepository<Lawyer> lawyersRepository,
            IDeletableEntityRepository<CV> cvsRepository,
            IDeletableEntityRepository<Judge> judgesRepository,
            IDeletableEntityRepository<Witness> witnessesRepository,
            IDeletableEntityRepository<Prosecutor> prosecutorsRepository,
            IDeletableEntityRepository<Defendant> defendantsRepository,
            IDeletableEntityRepository<Guard> guardsRepository,
            IDeletableEntityRepository<JuryMember> juryMembersRepository)
        {
            this.lawyersRepository = lawyersRepository ?? throw new ArgumentNullException(nameof(lawyersRepository));
            this.cvsRepository = cvsRepository ?? throw new ArgumentNullException(nameof(cvsRepository));
            this.judgesRepository = judgesRepository;
            this.witnessesRepository = witnessesRepository;
            this.prosecutorsRepository = prosecutorsRepository;
            this.defendantsRepository = defendantsRepository;
            this.guardsRepository = guardsRepository;
            this.juryMembersRepository = juryMembersRepository;
        }

        public async Task AddLawyer(LawyerInputModel lawyer)
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
        }

        public async Task AddJudge(JudgeInputModel judge)
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
        }

        public async Task AddWitness(WitnessInputModel witness)
        {
            var realWitness = new Witness
            {
                FirstName = witness.FirstName,
                LastName = witness.LastName,
                ImageUrl = witness.ImageUrl,
            };

            await this.witnessesRepository.AddAsync(realWitness);
            await this.witnessesRepository.SaveChangesAsync();
        }

        public async Task AddProsecutor(ProsecutorInputModel prosecutor)
        {
            var realProsecutor = new Prosecutor
            {
                FirstName = prosecutor.FirstName,
                LastName = prosecutor.LastName,
                ImageUrl = prosecutor.ImageUrl,
            };

            await this.prosecutorsRepository.AddAsync(realProsecutor);
            await this.prosecutorsRepository.SaveChangesAsync();
        }

        public async Task AddDefendant(DefendantInputModel defendant)
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
        }

        public async Task AddGuard(GuardInputModel guard)
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
        }

        public async Task AddJuryMember(JuryMemberInputModel juryMember)
        {
            var realJuryMember = new JuryMember
            {
                FirstName = juryMember.FirstName,
                LastName = juryMember.LastName,
            };

            await this.juryMembersRepository.AddAsync(realJuryMember);
            await this.juryMembersRepository.SaveChangesAsync();
        }
    }
}
