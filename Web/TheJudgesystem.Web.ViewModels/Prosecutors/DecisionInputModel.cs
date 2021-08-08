using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheJudgesystem.Web.ViewModels.Prosecutors
{
    public class DecisionInputModel
    {
        [Required]
        public string ProsecutorDecision { get; set; }

        public int CaseId { get; set; }

    }
}
