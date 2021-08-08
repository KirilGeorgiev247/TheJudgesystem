using System;
using System.Collections.Generic;
using System.Text;

namespace TheJudgesystem.Web.ViewModels.Defendants
{
    public class InfoViewModel
    {
        public MyLawyerViewModel Lawyer { get; set; }

        public MyCaseViewModel Case { get; set; }

        public string ImageUrl { get; set; }
    }
}
