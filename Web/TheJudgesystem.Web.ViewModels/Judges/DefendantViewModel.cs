using System;
using System.Collections.Generic;
using System.Text;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Services.Mapping;

namespace TheJudgesystem.Web.ViewModels.Judges
{
    public class DefendantViewModel : IMapFrom<Defendant>
    {

        public bool IsGuilty { get; set; }

    }
}
