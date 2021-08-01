namespace TheJudgesystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TheJudgesystem.Data.Common.Models;

    public class Lawyer : BaseDeletableModel<int>
    {
        public Lawyer()
        {
            this.Cases = new HashSet<Case>();
            this.Clients = new HashSet<Defendant>();
        }

        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public decimal? Rating { get; set; }

        public CV CV { get; set; }

        [Required]
        [ForeignKey(nameof(CV))]
        public int CVId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public decimal Salary { get; set; }

        public ICollection<Case> Cases { get; set; }

        public ICollection<Defendant> Clients { get; set; }
    }
}
