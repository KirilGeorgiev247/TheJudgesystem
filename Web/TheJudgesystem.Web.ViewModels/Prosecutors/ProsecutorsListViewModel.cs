using System;
using System.Collections.Generic;
using System.Text;

namespace TheJudgesystem.Web.ViewModels.Prosecutors
{
    public class ProsecutorsListViewModel : PagingViewModel
    {

        public ICollection<CaseInList> Cases { get; set; }

    }
}
