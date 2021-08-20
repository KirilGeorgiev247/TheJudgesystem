using System;
using System.Collections.Generic;
using System.Text;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Services.Mapping;

namespace TheJudgesystem.Web.ViewModels.Lawyers
{
    public class DefendantViewModel : IMapFrom<Defendant>
    {

        public bool IsDeleted { get; set; }

    }
}
