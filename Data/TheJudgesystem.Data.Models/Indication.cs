namespace TheJudgesystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TheJudgesystem.Data.Common.Models;

    public class Indication : BaseDeletableModel<int>
    {
        [Required]
        public string Description { get; set; }

        public Case Case { get; set; }

        [ForeignKey(nameof(Case))]
        public int? CaseId { get; set; }

        public Witness Witness { get; set; }

        [ForeignKey(nameof(Witness))]
        public int? WitnessId { get; set; }
    }
}
