using System;
using System.Collections.Generic;
using System.Text;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Services.Mapping;

namespace TheJudgesystem.Web.ViewModels.Lawyers
{
    public class CaseInList : IMapFrom<Case>
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string DefendantImageUrl { get; set; }

        public string DefendantFirstName { get; set; }

        public string DefendantLastName { get; set; }

        public string DefendantCharges { get; set; }

        public DefendantViewModel Defendant { get; set; }
    }
}
