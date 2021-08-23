using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Services.Mapping;

namespace TheJudgesystem.Services.Data.Tests.TestModels.DefendantsService
{
    public class MyImageTestModel : IMapFrom<Defendant>
    {

        public string ImageUrl { get; set; }

    }
}
