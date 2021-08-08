using System;
using System.Collections.Generic;
using System.Text;

namespace TheJudgesystem.Web.ViewModels.Lawyers
{
    public class CasesListViewModel : PagingViewModel
    {

        public IEnumerable<CaseInList> Cases { get; set; }

    }
}
