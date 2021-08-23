using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheJudgesystem.Web.ViewModels;

namespace TheJudgesystem.Services.Data.Tests.TestModels.DefendantsService
{
    public class LawyersInListTestModel : PagingViewModel
    {

        public ICollection<LawyerInListTestModel> Lawyers { get; set; }

    }
}
