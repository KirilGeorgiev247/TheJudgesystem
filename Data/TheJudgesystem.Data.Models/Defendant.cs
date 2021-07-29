namespace TheJudgesystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TheJudgesystem.Data.Common.Models;

    // ToDo: Validation
    public class Defendant : BaseDeletableModel<int>
    {
        public Defendant()
        {
            this.IsGuilty = false;
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public Case Case { get; set; }

        [ForeignKey(nameof(Case))]
        public int? CaseId { get; set; }

        public Lawyer Lawyer { get; set; }

        [ForeignKey(nameof(Lawyer))]
        public int? LawyerId { get; set; }

        public Cell Cell { get; set; }

        [ForeignKey(nameof(Cell))]
        public int? CellId { get; set; }

        [Required]
        public string Job { get; set; }

        public bool IsGuilty { get; set; }

        [Required]
        public string Charges { get; set; }

        public Prison Prison { get; set; }

        [ForeignKey(nameof(Prison))]
        public int? PrisonId { get; set; }
    }
}
