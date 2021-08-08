using System;
using System.Collections.Generic;
using System.Text;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Services.Mapping;

namespace TheJudgesystem.Web.ViewModels.Defendants
{
    public class CaseIndicationViewModel : IMapFrom<Indication>
    {
        public string Description { get; set; }

        public string WitnessFirstName { get; set; }

        public string WitnessLastName { get; set; }

        public int? WitnessId { get; set; }

    }
}
