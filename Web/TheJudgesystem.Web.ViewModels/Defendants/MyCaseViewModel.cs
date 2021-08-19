using System;
using System.Collections.Generic;
using System.Text;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Services.Mapping;

namespace TheJudgesystem.Web.ViewModels.Defendants
{
    public class MyCaseViewModel : IMapFrom<Case>
    {
        public int Id { get; set; }

        public string LawyerFirstName { get; set; }

        public string LawyerLastName { get; set; }

        public int? LawyerId { get; set; }

        public string ProsecutorFirstName { get; set; }

        public string ProsecutorLastName { get; set; }

        public int? ProsecutorId { get; set; }

        public string Description { get; set; }

        public DateTime? Date { get; set; }

        public bool IsSolved { get; set; }

        public IEnumerable<CaseIndicationViewModel> Indications { get; set; }

        public string JuryPronouncement { get; set; }

    }
}
