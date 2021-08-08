using System;
using System.Collections.Generic;
using System.Text;

namespace TheJudgesystem.Web.ViewModels.Witnesses
{
    public class WitnessesListViewModel : PagingViewModel
    {

        public IEnumerable<CaseInList> Cases { get; set; }

    }
}
