namespace TheJudgesystem.Web.ViewModels.Roles
{
    using System.ComponentModel.DataAnnotations;

    public class WitnessInputModel
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

    }
}
