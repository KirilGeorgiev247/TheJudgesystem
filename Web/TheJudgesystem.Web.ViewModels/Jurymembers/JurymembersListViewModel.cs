using System;
using System.Collections.Generic;
using System.Text;

namespace TheJudgesystem.Web.ViewModels.JuryMembers
{
    public class JurymembersListViewModel : PagingViewModel
    {

        public ICollection<CaseInList> Cases { get; set; }

    }
}
