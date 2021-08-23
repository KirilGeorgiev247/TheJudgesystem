using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TheJudgesystem.Data;
using TheJudgesystem.Data.Common.Repositories;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Data.Repositories;

using Microsoft.EntityFrameworkCore;

using Moq;

using Xunit;
using TheJudgesystem.Services.Data.StuffServices;
using TheJudgesystem.Services.Data.PeopleServices;
using Microsoft.AspNetCore.Identity;
using TheJudgesystem.Web.ViewModels.Cases;
using System.Security.Claims;

namespace TheJudgesystem.Services.Data.Tests
{
    public class CasesServiceTests
    {

        //[Fact]
        //public async Task GetCasesCountShouldReturnCorrectNumberUsingDbContext()
        //{
        //    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        //        .UseInMemoryDatabase(databaseName: "TheJudgesystem").Options;
        //    using var dbContext = new ApplicationDbContext(options);
        //    dbContext.Cases.Add(new Case());
        //    await dbContext.SaveChangesAsync();

        //    //    private readonly IDeletableEntityRepository<Lawyer> lawyersRepository;
        //    //private readonly IDeletableEntityRepository<Defendant> defendantsRepository;
        //    //private readonly IDeletableEntityRepository<Case> casesRepository;
        //    //private readonly IUsersService usersService;
        //    //private readonly IDefendantService defendantService;

        //    //private readonly UserManager<ApplicationUser> userManager;
        //    //private readonly RoleManager<ApplicationRole> roleManager;

        //    var userManager = new Mock<UserManager<ApplicationUser>>();
        //    var roleManager = new Mock<RoleManager<ApplicationRole>>();
        //    using var lawyersRepository = new EfDeletableEntityRepository<Lawyer>(dbContext);
        //    using var casesRepository = new EfDeletableEntityRepository<Case>(dbContext);
        //    using var defendantsRepository = new EfDeletableEntityRepository<Defendant>(dbContext);
        //    var usersService = new Mock<UsersService>(userManager, roleManager);
        //    var defendantsService = new Mock<DefendantService>(usersService, lawyersRepository, defendantsRepository, casesRepository);
        //    var service = new Mock<CasesService>(lawyersRepository, defendantsRepository, casesRepository, usersService, defendantsService);
        //    var input = new Mock<CaseInputModel>();
        //    var user = new Mock<ClaimsPrincipal>();
        //    Assert.Equal(1, service.Object.AddCase(input.Object, user.Object, 1));
        //}

    }
}
