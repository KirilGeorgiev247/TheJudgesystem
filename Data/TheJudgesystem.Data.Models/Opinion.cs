using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TheJudgesystem.Data.Common.Enumerations;
using TheJudgesystem.Data.Common.Models;

namespace TheJudgesystem.Data.Models
{
    public class Opinion : BaseDeletableModel<int>
    {
        [Required]
        public string Description { get; set; }

        public GuiltinessEnumeration Guiltiness { get; set; }

        public Case Case { get; set; }

        [ForeignKey(nameof(Case))]
        public int? CaseId { get; set; }

        public Jurymember Jurymember { get; set; }

        [ForeignKey(nameof(Jurymember))]
        public int? JurymemberId { get; set; }

        public Jury Jury { get; set; }
        
        [ForeignKey(nameof(Jury))]
        public int? JuryId { get; set; }
    }
}
