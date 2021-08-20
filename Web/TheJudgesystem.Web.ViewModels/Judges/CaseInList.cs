﻿using System;
using System.Collections.Generic;
using System.Text;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Services.Mapping;

namespace TheJudgesystem.Web.ViewModels.Judges
{
    public class CaseInList : IMapFrom<Case>
    {

        public int Id { get; set; }

        public string Description { get; set; }

        public string DefendantImageUrl { get; set; }

        public string DefendantFirstName { get; set; }

        public string DefendantLastName { get; set; }

        public string DefendantCharges { get; set; }

        public string LawyerDefence { get; set; }

        public string ProsecutorDecision { get; set; }

        public ICollection<IndicationViewModel> Indications { get; set; }

        public string JuryPronouncement { get; set; }

        public string JudgeDecision { get; set; }

        public DefendantViewModel Defendant { get; set; }
    }
}
