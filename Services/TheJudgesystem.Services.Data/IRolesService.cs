namespace TheJudgesystem.Services.Data
{
    using System.Threading.Tasks;

    using TheJudgesystem.Web.ViewModels.Roles;

    public interface IRolesService
    {

        public Task AddLawyer(LawyerInputModel lawyer);

        public Task AddJudge(JudgeInputModel judge);

        public Task AddWitness(WitnessInputModel witness);

        public Task AddProsecutor(ProsecutorInputModel prosecutor);

        public Task AddDefendant(DefendantInputModel defendant);

        public Task AddGuard(GuardInputModel guard);

        public Task AddJuryMember(JuryMemberInputModel juryMember);
    }
}
