using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Services.Mapping;

namespace TheJudgesystem.Web.ViewModels.JuryMembers
{
    public class IndicationViewModel : IMapFrom<Indication>
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        public int CaseId { get; set; }

        public int WitnessId { get; set; }

        public string WitnessFirstName { get; set; }

        public string WitnessLastName { get; set; }
    }
}
