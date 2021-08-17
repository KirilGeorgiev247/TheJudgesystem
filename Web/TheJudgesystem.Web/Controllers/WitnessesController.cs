using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheJudgesystem.Services.Data.PeopleServices;
using TheJudgesystem.Web.ViewModels.Witnesses;

namespace TheJudgesystem.Web.Controllers
{
    [Authorize(Roles = "Witness")]
    public class WitnessesController : Controller
    {
        private readonly IWitnessesService witnessesService;

        public WitnessesController(
            IWitnessesService witnessesService)
        {
            this.witnessesService = witnessesService;
        }


        [HttpGet]
        public async Task<IActionResult> Cases(int id = 1)
        {
            var itemsCount = 4;

            var cases = new WitnessesListViewModel
            {
                ItemsPerPage = itemsCount,
                Cases = await this.witnessesService.GetCases(this.User, id, itemsCount),
                PageNumber = id,
                EntityCount = await this.witnessesService.GetCasesCount(this.User),
            };

            return this.View(cases);
        }

        [HttpGet]
        public IActionResult Indications(int id)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Indications(IndicationInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.witnessesService.AddIndication(input, id, this.User);

            return this.Redirect("/Witnesses/Cases");
        }
    }
}
