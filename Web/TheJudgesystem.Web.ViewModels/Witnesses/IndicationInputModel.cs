using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheJudgesystem.Web.ViewModels.Witnesses
{
    public class IndicationInputModel
    {
        [Required]
        public string WitnessIndications { get; set; }

    }
}
