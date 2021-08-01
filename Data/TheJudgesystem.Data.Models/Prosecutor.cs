namespace TheJudgesystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TheJudgesystem.Data.Common.Models;

    public class Prosecutor : BaseDeletableModel<int>
    {
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public Case Case { get; set; }

        [ForeignKey(nameof(Case))]
        public int? CaseId { get; set; }

        public Defendant Defendant { get; set; }

        [ForeignKey(nameof(Defendant))]
        public int? DefendantId { get; set; }

        public string Raises { get; set; }
    }
}
