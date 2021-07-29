namespace TheJudgesystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TheJudgesystem.Data.Common.Models;

    public class Prosecutor : BaseDeletableModel<int>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public Case Case { get; set; }

        [Required]
        [ForeignKey(nameof(Case))]
        public int CaseId { get; set; }

        public Defendant Defendant { get; set; }

        [Required]
        [ForeignKey(nameof(Defendant))]
        public int DefendantId { get; set; }

        [Required]
        public string Raises { get; set; }
    }
}
