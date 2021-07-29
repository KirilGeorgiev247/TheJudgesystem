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
        private readonly IDeletableEntityRepository<CV> cVsRepository;

        public RolesService(
            IDeletableEntityRepository<Lawyer> lawyersRepository,
            IDeletableEntityRepository<CV> CVsRepository)
        {
            this.lawyersRepository = lawyersRepository ?? throw new ArgumentNullException(nameof(lawyersRepository));
            this.cVsRepository = CVsRepository ?? throw new ArgumentNullException(nameof(cVsRepository));
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

            if (lawyer.Achievements.Length < 5)
            {
                realCV.Achievements = "Not given!";
            }
            else
            {
                realCV.Achievements = lawyer.Achievements;
            }

            await this.cVsRepository.AddAsync(realCV);
            await this.cVsRepository.SaveChangesAsync();

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
    }
}
