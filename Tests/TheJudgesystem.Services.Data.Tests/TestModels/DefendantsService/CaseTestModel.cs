using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Services.Mapping;

namespace TheJudgesystem.Services.Data.Tests.TestModels.DefendantsService
{
    public class CaseTestModel : IMapFrom<Case>
    {
        public int Id { get; set; }

        public string LawyerDefence { get; set; }

        public string ProsecutorDecision { get; set; }

        public string JudgeDecision { get; set; }
    }
}
