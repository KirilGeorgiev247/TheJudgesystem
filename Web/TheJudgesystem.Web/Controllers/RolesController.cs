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
        [Authorize]
        public IActionResult Lawyer()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
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

        [HttpGet]
        [Authorize]
        public IActionResult Judge()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Judge(JudgeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.rolesService.AddJudge(input);

            // User becomes judge

            return this.Redirect("/Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Witness()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Witness(WitnessInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.rolesService.AddWitness(input);

            // User becomes witness

            return this.Redirect("/Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Prosecutor()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Prosecutor(ProsecutorInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.rolesService.AddProsecutor(input);

            // User becomes prosecutor

            return this.Redirect("/Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Defendant()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Defendant(DefendantInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.rolesService.AddDefendant(input);

            // User becomes defendant

            return this.Redirect("/Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Guard()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Guard(GuardInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.rolesService.AddGuard(input);

            // User becomes guard

            return this.Redirect("/Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult JuryMember()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> JuryMember(JuryMemberInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.rolesService.AddJuryMember(input);

            // User becomes juryMember

            return this.Redirect("/Home");
        }

    }
}
