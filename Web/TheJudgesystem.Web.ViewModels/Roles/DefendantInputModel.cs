namespace TheJudgesystem.Web.ViewModels.Roles
{
    using System.ComponentModel.DataAnnotations;

    public class DefendantInputModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Job { get; set; }

        public bool IsGuilty { get; set; }

        [Required]
        public string Charges { get; set; }
    }
}
