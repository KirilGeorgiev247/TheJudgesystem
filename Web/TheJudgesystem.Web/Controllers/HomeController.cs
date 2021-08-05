namespace TheJudgesystem.Web.Controllers
{
    using System.Diagnostics;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
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
            if (this.usersService.IsInThisRole("Defendant", this.User).Result)
            {
                return this.Redirect("/Defendant/Lawyers");
            }
            else if (this.usersService.IsInThisRole("Lawyer", this.User).Result)
            {
                return this.Redirect("/Lawyers/Cases");
            }
            else if (this.usersService.IsInThisRole("Prosecutor", this.User).Result)
            {
                return this.Redirect("/Prosecutors/Cases");
            }

            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Role()
        {
            if (this.User.IsInRole("Defendant"))
            {
                return this.Redirect("/Defendant/Lawyers");
            }

            return this.View();
        }

        public IActionResult Test()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var obj = new
            {
                UserId = userId,
            };

            return this.Json(obj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
