﻿namespace TheJudgesystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TheJudgesystem.Data.Common.Models;

    public class Case : BaseDeletableModel<int>
    {
        public Case()
        {
            this.IsSolved = false;
        }

        public Defendant Defendant { get; set; }

        [Required]
        [ForeignKey(nameof(Defendant))]
        public int DefendantId { get; set; }

        public Lawyer Lawyer { get; set; }

        [Required]
        [ForeignKey(nameof(Lawyer))]
        public int LawyerId { get; set; }

        public Jury Jury { get; set; }

        [Required]
        [ForeignKey(nameof(Jury))]
        public int JuryId { get; set; }

        public Prosecutor Prosecutor { get; set; }

        [Required]
        [ForeignKey(nameof(Prosecutor))]
        public int ProsecutorId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public bool IsSolved { get; set; }
    }
}
