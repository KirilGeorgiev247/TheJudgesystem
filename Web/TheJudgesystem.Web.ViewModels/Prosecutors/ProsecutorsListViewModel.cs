using System;
using System.Collections.Generic;
using System.Text;

namespace TheJudgesystem.Web.ViewModels.Prosecutors
{
    public class ProsecutorsListViewModel : PagingViewModel
    {

        public IEnumerable<CaseInList> Cases { get; set; }

    }
}
