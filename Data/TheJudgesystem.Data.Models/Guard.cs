namespace TheJudgesystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TheJudgesystem.Data.Common.Models;

    public class Guard : BaseDeletableModel<int>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime StartOfTheWorkDay { get; set; }

        public DateTime EndOfTheWorkDay { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public decimal Salary { get; set; }

        public Prison Prison { get; set; }

        [ForeignKey(nameof(Prison))]
        public int? PrisonId { get; set; }
    }
}
