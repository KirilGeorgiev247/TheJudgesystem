namespace TheJudgesystem.Web.ViewModels.Roles
{
    using System.ComponentModel.DataAnnotations;

    public class ProsecutorInputModel
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

    }
}
