using System;
using System.Collections.Generic;
using System.Text;
using TheJudgesystem.Data.Common.Enumerations;

namespace TheJudgesystem.Web.ViewModels.Jurymembers
{
    public class OpinionInputModel
    {

        public GuiltinessEnumeration Opinion { get; set; }

        public string Description { get; set; }

    }
}
