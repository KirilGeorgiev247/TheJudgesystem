namespace TheJudgesystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TheJudgesystem.Data.Common.Models;

    public class CV : BaseDeletableModel<int>
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
        public string BirthTown { get; set; }

        [Required]
        public string School { get; set; }

        [Required]
        public string University { get; set; }

        public string Achievements { get; set; }
    }
}
