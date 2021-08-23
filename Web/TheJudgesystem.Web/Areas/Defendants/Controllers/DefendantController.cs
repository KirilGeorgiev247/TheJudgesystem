namespace TheJudgesystem.Web.Areas.Defendants
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TheJudgesystem.Services.Data;
    using TheJudgesystem.Services.Data.PeopleServices;
    using TheJudgesystem.Web.ViewModels.Defendants;

    [Authorize(Roles = "Defendant")]
    [Area("Defendants")]
    public class DefendantController : Controller
    {
        private readonly IDefendantService defendantService;

        public DefendantController(
            IDefendantService defendantService)
        {
            this.defendantService = defendantService;
        }

        [HttpGet]
        public async Task<IActionResult> Lawyers(int id = 1)
        {
            var hasLawyer = await this.defendantService.HasLawyer(this.User);

            if (hasLawyer)
            {
                return this.Redirect("/Defendant/Info");
            }

            var itemsCount = 4;

            var lawyers = new LawyersListViewModel
            {
                ItemsPerPage = itemsCount,
                Lawyers = await this.defendantService.GetLawyers<LawyerInList>(id, itemsCount),
                PageNumber = id,
                EntityCount = await this.defendantService.GetCount(),
            };

            return this.View(lawyers);
        }

        [HttpGet]
        public async Task<IActionResult> Info()
        {
            var infoModel = await this.defendantService.GetInfo<InfoViewModel>(this.User);

            return this.View(infoModel);
        }
    }
}
