namespace TheJudgesystem.Web.ViewModels.Roles
{
    using System.ComponentModel.DataAnnotations;

    public class JuryMemberInputModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
