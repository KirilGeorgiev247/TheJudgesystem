using System;
using System.Collections.Generic;
using System.Text;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Services.Mapping;

namespace TheJudgesystem.Web.ViewModels.Defendants
{
    public class MyImageViewModel : IMapFrom<Defendant>
    {
        public string ImageUrl { get; set; }
    }
}
