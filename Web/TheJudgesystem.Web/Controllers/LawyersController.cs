using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheJudgesystem.Services.Data.PeopleServices;
using TheJudgesystem.Web.ViewModels.Lawyers;

namespace TheJudgesystem.Web.Controllers
{
    [Authorize(Roles = "Lawyer")]
    public class LawyersController : Controller
    {
        private readonly ILawyersService lawyersService;

        public LawyersController(
            ILawyersService lawyersService)
        {
            this.lawyersService = lawyersService;
        }

        [HttpGet]
        public async Task<IActionResult> Cases(int id = 1)
        {
            var itemsCount = 4;

            var cases = new CasesListViewModel
            {
                ItemsPerPage = itemsCount,
                Cases = await this.lawyersService.GetCases(this.User, id, itemsCount),
                PageNumber = id,
                EntityCount = await this.lawyersService.GetCasesCount(),
            };

            return this.View(cases);
        }

        [HttpGet]
        public IActionResult Defence(int id)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Defence(DefenceInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.lawyersService.AddDefence(input, id);

            return this.Redirect("/Lawyers/Cases");
        }
    }
}
