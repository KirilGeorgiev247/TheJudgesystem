namespace TheJudgesystem.Web.ViewModels.Roles
{
    using System.ComponentModel.DataAnnotations;

    public class GuardInputModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public decimal Salary { get; set; }
    }
}
