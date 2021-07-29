namespace TheJudgesystem.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TheJudgesystem.Services.Data;
    using TheJudgesystem.Web.ViewModels.Roles;

    public class RolesController : Controller
    {
        private readonly IRolesService rolesService;

        public RolesController(IRolesService rolesService)
        {
            this.rolesService = rolesService;
        }

        [HttpGet]
        public IActionResult Lawyer()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Lawyer(LawyerInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.rolesService.AddLawyer(input);

            // User becomes lawyer

            return this.Redirect("/Home");
        }

    }
}
