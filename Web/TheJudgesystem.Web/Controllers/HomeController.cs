namespace TheJudgesystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TheJudgesystem.Common;
    using TheJudgesystem.Services.Data;
    using TheJudgesystem.Web.ViewModels;

    public class HomeController : BaseController
    {
        private readonly IUsersService usersService;

        public HomeController(
            IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            return this.usersService.GetApplicaionUserRole(this.User) switch
            {
                GlobalConstants.DefendantRole => this.Redirect("/Defendant/Lawyers"),
                GlobalConstants.LawyerRole => this.Redirect("/Lawyers/Cases"),
                GlobalConstants.ProsecutorRole => this.Redirect("/Prosecutors/Cases"),
                GlobalConstants.WitnessRole => this.Redirect("/Witnesses/Cases"),
                GlobalConstants.JurymemberRole => this.Redirect("/Jurymembers/Cases"),
                _ => this.View(),
            };
        }

        [Authorize]
        public IActionResult Role()
        {
            if (this.usersService.IsInThisRole(GlobalConstants.DefendantRole, this.User).Result)
            {
                return this.Redirect("/Defendant/Lawyers");
            }
            else if (this.usersService.IsInThisRole(GlobalConstants.JurymemberRole, this.User).Result)
            {
                return this.Redirect("/Jurymembers/Cases");
            }

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
