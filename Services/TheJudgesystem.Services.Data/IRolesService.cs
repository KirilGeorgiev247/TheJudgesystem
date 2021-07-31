namespace TheJudgesystem.Services.Data
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using TheJudgesystem.Web.ViewModels.Roles;

    public interface IRolesService
    {

        public Task AddLawyer(LawyerInputModel lawyer, ClaimsPrincipal user);

        public Task AddJudge(JudgeInputModel judge, ClaimsPrincipal user);

        public Task AddWitness(WitnessInputModel witness, ClaimsPrincipal user);

        public Task AddProsecutor(ProsecutorInputModel prosecutor, ClaimsPrincipal user);

        public Task AddDefendant(DefendantInputModel defendant, ClaimsPrincipal user);

        public Task AddGuard(GuardInputModel guard, ClaimsPrincipal user);

        public Task AddJuryMember(JuryMemberInputModel juryMember, ClaimsPrincipal user);
    }
}
