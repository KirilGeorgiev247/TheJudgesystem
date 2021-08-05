namespace TheJudgesystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TheJudgesystem.Data.Common.Models;

    public class Case : BaseDeletableModel<int>
    {
        public Case()
        {
            this.IsSolved = false;
            this.Indications = new HashSet<Indication>();
        }

        public Defendant Defendant { get; set; }

        [Required]
        [ForeignKey(nameof(Defendant))]
        public int DefendantId { get; set; }

        public Lawyer Lawyer { get; set; }

        [ForeignKey(nameof(Lawyer))]
        public int? LawyerId { get; set; }

        public Jury Jury { get; set; }

        [ForeignKey(nameof(Jury))]
        public int? JuryId { get; set; }

        public Prosecutor Prosecutor { get; set; }

        [ForeignKey(nameof(Prosecutor))]
        public int? ProsecutorId { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime? Date { get; set; }

        public bool IsSolved { get; set; }

        public ICollection<Indication> Indications { get; set; }

        public string LawyerDefence { get; set; }

        public string ProsecutorDecision { get; set; }

        public string JudgeDecision { get; set; }

    }
}
