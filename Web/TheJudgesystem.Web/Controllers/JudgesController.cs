using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheJudgesystem.Common;
using TheJudgesystem.Services.Data.PeopleServices;
using TheJudgesystem.Web.ViewModels.Judges;

namespace TheJudgesystem.Web.Controllers
{
    [Authorize(Roles = "Judge")]
    public class JudgesController : Controller
    {
        private readonly IJudgesService judgesService;

        public JudgesController(
            IJudgesService judgesService)
        {
            this.judgesService = judgesService;
        }

        [HttpGet]
        public async Task<IActionResult> Defendants(int id = 1)
        {
            var itemsCount = 1;

            var cases = new JudgesListViewModel
            {
                ItemsPerPage = itemsCount,
                Cases = await this.judgesService.GetCases(this.User, id, itemsCount),
                PageNumber = id,
                EntityCount = await this.judgesService.GetCasesCount(),
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
                    await this.judgesService.DecideForGuilty(input, id, this.User);
                    break;
                case GlobalConstants.NotGuilty:
                    await this.judgesService.DecideForNotGuilty(input, id, this.User);
                    break;
                case GlobalConstants.Fee:
                    await this.judgesService.DecideForFee(input, id, this.User);
                    break;
                default:
                    break;
            }

            return this.Redirect("/Judges/Defendants");
        }

    }
}
