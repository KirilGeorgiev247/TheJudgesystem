namespace TheJudgesystem.Web.ViewModels.Roles
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TheJudgesystem.Data.Models;

    public class JudgeInputModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public string BirthTown { get; set; }

        [Required]
        public string School { get; set; }

        [Required]
        public string University { get; set; }

        public string Achievements { get; set; }

        public ICollection<Case> Cases { get; set; }
    }
}
