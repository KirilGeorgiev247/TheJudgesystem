namespace TheJudgesystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TheJudgesystem.Services.Data;
    using TheJudgesystem.Services.Data.PeopleServices;
    using TheJudgesystem.Web.ViewModels.Defendants;

    [Authorize(Roles = "Defendant")]
    public class DefendantController : Controller
    {
        private readonly IDefendantService defendantService;

        public DefendantController(
            IDefendantService defendantService)
        {
            this.defendantService = defendantService;
        }

        [HttpGet]
        public IActionResult Lawyers(int id = 1)
        {
            if (this.defendantService.HasLawyer(this.User))
            {
                return this.Redirect("/Defendant/Info");
            }

            var itemsCount = 6;

            var lawyers = new LawyersListViewModel
            {
                ItemsPerPage = itemsCount,
                Lawyers = this.defendantService.GetLawyers(id, itemsCount),
                PageNumber = id,
                EntityCount = this.defendantService.GetCount(),
            };

            return this.View(lawyers);
        }

        [HttpGet]
        public IActionResult Info()
        {
            var infoModel = this.defendantService.GetInfo(this.User);

            return this.View(infoModel);
        }
    }
}
