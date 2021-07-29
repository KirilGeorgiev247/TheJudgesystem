namespace TheJudgesystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TheJudgesystem.Data.Common.Models;

    public class Witness : BaseDeletableModel<int>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public Indication Indication { get; set; }

        [ForeignKey(nameof(Indication))]
        public int? IndicationId { get; set; }

        public Case Case { get; set; }

        [ForeignKey(nameof(Case))]
        public int? CaseId { get; set; }
    }
}
