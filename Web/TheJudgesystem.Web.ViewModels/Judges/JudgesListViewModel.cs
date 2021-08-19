using System;
using System.Collections.Generic;
using System.Text;

namespace TheJudgesystem.Web.ViewModels.Judges
{
    public class JudgesListViewModel : PagingViewModel
    {
        public ICollection<CaseInList> Cases { get; set; }

    }
}
