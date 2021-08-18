using System;
using System.Collections.Generic;
using System.Text;
using TheJudgesystem.Web.ViewModels.Jurymembers;

namespace TheJudgesystem.Web.ViewModels.Jurymember
{
    public class JurymembersListViewModel : PagingViewModel
    {

        public ICollection<CaseInList> Cases { get; set; }

    }
}
