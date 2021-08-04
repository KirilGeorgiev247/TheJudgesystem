namespace TheJudgesystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TheJudgesystem.Data.Common.Models;

    public class Judge : BaseDeletableModel<int>
    {
        public Judge()
        {
            this.Cases = new HashSet<Case>();
        }
        public string UserId { get; set; }


        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public CV CV { get; set; }

        [Required]
        [ForeignKey(nameof(CV))]
        public int CVId { get; set; }

        public ICollection<Case> Cases { get; set; }
    }
}
