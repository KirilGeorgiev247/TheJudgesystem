using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheJudgesystem.Web.ViewModels.Cases
{
    public class CaseInputModel
    {
        [Required]
        public string Description { get; set; }

        public int LawyerId { get; set; }

    }
}
