using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheJudgesystem.Common;
using TheJudgesystem.Services.Data.PeopleServices;
using TheJudgesystem.Web.ViewModels.Jurymembers;
using TheJudgesystem.Web.ViewModels.Jurymember;
using TheJudgesystem.Data.Common.Enumerations;

namespace TheJudgesystem.Web.Controllers
{
    [Authorize(Roles = GlobalConstants.JurymemberRole)]
    public class JurymembersController : Controller
    {
        private readonly IJurymembersService juryMembersService;

        public JurymembersController(
            IJurymembersService juryMembersService)
        {
            this.juryMembersService = juryMembersService;
        }

        [HttpGet]
        public async Task<IActionResult> Cases(int id = 1)
        {
            var itemsCount = 1;

            var cases = new JurymembersListViewModel
            {
                ItemsPerPage = itemsCount,
                Cases = await this.juryMembersService.GetCases(this.User, id, itemsCount),
                PageNumber = id,
                EntityCount = await this.juryMembersService.GetCasesCount(this.User),
            };

            return this.View(cases);
        }

        [HttpGet]
        public IActionResult Opinion(int id)
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Opinion(OpinionInputModel input, int id, string button)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            input.Opinion = (GuiltinessEnumeration)Enum.Parse(typeof(GuiltinessEnumeration), button, true);

            await this.juryMembersService.AddOpinion(input, id, this.User);

            return this.Redirect("/Jurymembers/Cases");
        }

    }
}
