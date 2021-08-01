namespace TheJudgesystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TheJudgesystem.Services.Data.PeopleServices;
    using TheJudgesystem.Web.ViewModels.Defendants;

    [Authorize(Policy = "IsDefendant")]
    public class DefendantController : Controller
    {
        private readonly IDefendantService defendantService;

        public DefendantController(IDefendantService defendantService)
        {
            this.defendantService = defendantService;
        }

        [HttpGet]
        public async Task<IActionResult> Lawyers(int id = 1)
        {
            var itemsCount = 6;

            var lawyers = new LawyersListViewModel
            {
                ItemsPerPage = itemsCount,
                Lawyers = this.defendantService.GetLawyers(id, itemsCount),
                PageNumber = id,
                LawyersCount = this.defendantService.GetCount(),
            };

            return this.View(lawyers);
        }

        public async Task<IActionResult> HireLawyer(int id)
        {
            await this.defendantService.HireLawyer(id, this.User);

            return this.Redirect("/Home");
        }

    }
}
