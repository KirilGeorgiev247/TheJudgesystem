using System;
using System.Collections.Generic;
using System.Text;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Services.Mapping;

namespace TheJudgesystem.Web.ViewModels.Judges
{
    public class JuryViewModel : IMapFrom<Jury>
    {

        public string Pronouncement { get; set; }

        public int CaseId { get; set; }

    }
}
