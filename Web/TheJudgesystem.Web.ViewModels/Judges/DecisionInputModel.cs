using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheJudgesystem.Web.ViewModels.Judges
{
    public class DecisionInputModel
    {
        [Required]
        public string JudgeDecision { get; set; }

        public int CaseId { get; set; }
    }
}
