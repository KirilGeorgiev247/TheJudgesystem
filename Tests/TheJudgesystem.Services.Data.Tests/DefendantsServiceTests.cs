namespace TheJudgesystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using MockQueryable.Moq;
    using Moq;
    using TheJudgesystem.Data;
    using TheJudgesystem.Data.Common.Repositories;
    using TheJudgesystem.Data.Models;
    using TheJudgesystem.Data.Repositories;
    using TheJudgesystem.Services.Data.PeopleServices;
    using TheJudgesystem.Services.Data.Tests.TestModels.DefendantsService;
    using TheJudgesystem.Services.Mapping;
    using Xunit;

    public class DefendantsServiceTests
    {
        private readonly Mock<ClaimsPrincipal> claim;

        private readonly DbContextOptionsBuilder<ApplicationDbContext> options;

        private readonly IDeletableEntityRepository<Defendant> defendantsRepository;
        private readonly Mock<IDeletableEntityRepository<Defendant>> mockedDefendantsRepository;
        private readonly Mock<IDeletableEntityRepository<Lawyer>> lawyersRepository;
        private readonly Mock<IDeletableEntityRepository<Case>> casesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        private readonly Mock<IUsersService> usersService;
        private readonly Mock<IDefendantService> mockedDefendantsService;

        private readonly IDefendantService defendantsService;

        public DefendantsServiceTests()
        {
            this.claim = new Mock<ClaimsPrincipal>();

            this.options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            this.lawyersRepository = new Mock<IDeletableEntityRepository<Lawyer>>();
            this.mockedDefendantsRepository = new Mock<IDeletableEntityRepository<Defendant>>();
            this.defendantsRepository =
                new EfDeletableEntityRepository<Defendant>(new ApplicationDbContext(this.options.Options));
            this.casesRepository = new Mock<IDeletableEntityRepository<Case>>();
            this.usersRepository =
                new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(this.options.Options));
            this.usersService = new Mock<IUsersService>();

            this.mockedDefendantsService = new Mock<IDefendantService>();

            this.defendantsService = new DefendantService(
                this.usersService.Object,
                this.lawyersRepository.Object,
                this.defendantsRepository,
                this.casesRepository.Object);

            AutoMapperConfig.RegisterMappings(typeof(LawyerTestModel).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(LawyersInListTestModel).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(LawyerInListTestModel).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(MyImageTestModel).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(CaseTestModel).Assembly);
        }

        [Fact]
        public async Task CheckIfGetCountReturnsCorrectResult()
        {

            var expectedCount = 4;

            var entities = new List<Lawyer>()
                {
                    new Lawyer() { Id = 1},
                    new Lawyer() { Id = 2},
                    new Lawyer() { Id = 3},
                    new Lawyer() { Id = 4},
                };

            var lawyersMock = entities.AsQueryable().BuildMock();

            this.lawyersRepository.Setup(x => x.AllAsNoTracking())
                .Returns(lawyersMock.Object);

            var lawyersCount = await this.defendantsService.GetCount();

            Assert.Equal(expectedCount, lawyersCount);
        }

        [Fact]
        public async Task CheckIfGetDefedantReturnsCorrectResult()
        {
            var userId = "Test";

            await this.usersRepository.AddAsync(new ApplicationUser { Id = userId });
            await this.usersRepository.SaveChangesAsync();

            var user = this.usersRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);

            this.usersService.Setup(x => x.GetApplicationUserId(this.claim.Object))
                .Returns(user.Result.Id);

            var expectedResult = new Defendant
            {
                UserId = userId,
            };

            await this.defendantsRepository.AddAsync(expectedResult);
            await this.defendantsRepository.SaveChangesAsync();

            var result = await this.defendantsService.GetDefendant(this.claim.Object);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task CheckIfGetLawyersReturnsCorrectResult()
        {
            var expectedCount = 4;

            var entities = new List<Lawyer>()
                {
                    new Lawyer() { Id = 1},
                    new Lawyer() { Id = 2},
                    new Lawyer() { Id = 3},
                    new Lawyer() { Id = 4},
                };

            var lawyersMock = entities.AsQueryable().BuildMock();

            this.lawyersRepository.Setup(x => x.All())
                .Returns(lawyersMock.Object);

            var lawyers = await this.defendantsService.GetLawyers<LawyerInListTestModel>(1);

            Assert.Equal(expectedCount, lawyers.Count);
        }

        [Fact]
        public async Task CheckIfGetMyImageReturnsCorrectResult()
        {
            var userId = "UserId";
            var expectedResult = "Test";

            var defendant = new Defendant()
            {
                UserId = userId,
                ImageUrl = expectedResult,
            };

            await this.defendantsRepository.AddAsync(defendant);
            await this.defendantsRepository.SaveChangesAsync();

            this.usersService.Setup(x => x.GetApplicationUserId(this.claim.Object))
                .Returns(userId);

            var image = await this.defendantsService.GetMyImage<MyImageTestModel>(this.claim.Object);

            Assert.Equal(expectedResult, image.ImageUrl);
        }

        [Fact]
        public async Task CheckIfHasLawyerReturnsCorrectResult()
        {
            var userId = "UserId";

            var defendant = new Defendant()
            {
                UserId = userId,
                LawyerId = 1,
            };

            await this.defendantsRepository.AddAsync(defendant);
            await this.defendantsRepository.SaveChangesAsync();

            this.usersService.Setup(x => x.GetApplicationUserId(this.claim.Object))
                .Returns(userId);

            var result = await this.defendantsService.HasLawyer(this.claim.Object);

            Assert.True(result);
        }

        [Fact]
        public async Task CheckIfGetMyLawyerReturnsCorrectResult()
        {
            var userId = "Test";
            var lawyerId = 1;
            var firstName = "FirstName";
            var lastName = "LastName";

            var defendant = new Defendant
            {
                UserId = userId,
                LawyerId = lawyerId,
            };

            await this.defendantsRepository.AddAsync(defendant);
            await this.defendantsRepository.SaveChangesAsync();

            var lawyers = new List<Lawyer>()
            {
                new Lawyer()
                {
                    Id = lawyerId,
                    FirstName = firstName,
                    LastName = lastName,
                },
            };

            var lawyersMock = lawyers.AsQueryable().BuildMock();

            this.lawyersRepository.Setup(x => x.All())
                .Returns(lawyersMock.Object);

            this.usersService.Setup(x => x.GetApplicationUserId(this.claim.Object))
                .Returns(userId);

            var lawyer = await this.defendantsService.GetMyLawyer<LawyerTestModel>(this.claim.Object);

            Assert.Equal(lawyerId, lawyer.Id);
            Assert.Equal(firstName, lawyer.FirstName);
            Assert.Equal(lastName, lawyer.LastName);
        }

        [Fact]
        public async Task CheckIfGetMyCaseReturnsCorrectResult()
        {
            var defendantId = 1;
            var userId = "Test";
            var caseId = 1;
            var lawyerDefence = "Defence";
            var prosecutorDecision = "ProsecutorDecision";
            var judgeDecision = "JudgeDecision";


            var defendant = new Defendant
            {
                Id = defendantId,
                UserId = userId,
                CaseId = caseId,
            };

            await this.defendantsRepository.AddAsync(defendant);
            await this.defendantsRepository.SaveChangesAsync();

            var cases = new List<Case>()
            {
                new Case()
                {
                    Id = caseId,
                    LawyerDefence = lawyerDefence,
                    ProsecutorDecision = prosecutorDecision,
                    JudgeDecision = judgeDecision,
                    DefendantId = defendantId,
                },
            };

            var casesMock = cases.AsQueryable().BuildMock();

            this.casesRepository.Setup(x => x.All())
                .Returns(casesMock.Object);

            this.usersService.Setup(x => x.GetApplicationUserId(this.claim.Object))
                .Returns(userId);

            var @case = await this.defendantsService.GetMyCase<CaseTestModel>(this.claim.Object);

            Assert.Equal(caseId, @case.Id);
            Assert.Equal(lawyerDefence, @case.LawyerDefence);
            Assert.Equal(prosecutorDecision, @case.ProsecutorDecision);
            Assert.Equal(judgeDecision, @case.JudgeDecision);
        }
    }
}