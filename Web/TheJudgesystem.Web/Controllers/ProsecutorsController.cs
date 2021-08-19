using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheJudgesystem.Common;
using TheJudgesystem.Services.Data.PeopleServices;
using TheJudgesystem.Web.ViewModels.Prosecutors;

namespace TheJudgesystem.Web.Controllers
{
    [Authorize(Roles = "Prosecutor")]
    public class ProsecutorsController : Controller
    {
        private readonly IProsecutorsService prosecutorsService;

        public ProsecutorsController(
            IProsecutorsService prosecutorsService)
        {
            this.prosecutorsService = prosecutorsService;
        }

        [HttpGet]
        public async Task<IActionResult> Cases(int id = 1)
        {
            var itemsCount = 1;

            var cases = new ProsecutorsListViewModel
            {
                ItemsPerPage = itemsCount,
                Cases = await this.prosecutorsService.GetCases(this.User, id, itemsCount),
                PageNumber = id,
                EntityCount = await this.prosecutorsService.GetCasesCount(),
            };

            return this.View(cases);
        }

        [HttpGet]
        public IActionResult Decision(int id)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Decision(DecisionInputModel input, int id, string button)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            switch (button)
            {
                case GlobalConstants.Guilty:
                    await this.prosecutorsService.DecideForGuilty(input, id, this.User);
                    break;
                case GlobalConstants.NotGuilty:
                    await this.prosecutorsService.DecideForNotGuilty(input, id, this.User);
                    break;
                case GlobalConstants.Fee:
                    await this.prosecutorsService.DecideForFee(input, id, this.User);
                    break;
                default:
                    break;
            }

            return this.Redirect("/Prosecutors/Cases");
        }

        //[HttpPost]
        //public async Task<IActionResult> Defence(DefenceInputModel input, int id)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View();
        //    }

        //    await this.lawyersService.AddDefence(input, id);

        //    return this.Redirect("/Lawyers/Cases");
        //}

    }
}
