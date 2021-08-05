using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheJudgesystem.Services.Data.StuffServices;
using TheJudgesystem.Web.ViewModels.Cases;

namespace TheJudgesystem.Web.Controllers
{
    public class CasesController : Controller
    {
        private readonly ICasesService casesService;

        public CasesController(ICasesService casesService)
        {
            this.casesService = casesService;
        }

        [HttpGet]
        public IActionResult CreateCase()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCase(CaseInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            input.LawyerId = id;

            await this.casesService.AddCase(input, this.User, id);

            return this.Redirect("/Defendant/Lawyers");
        }

    }
}
