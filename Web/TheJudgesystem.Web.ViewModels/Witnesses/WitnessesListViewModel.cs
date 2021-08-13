using System;
using System.Collections.Generic;
using System.Text;

namespace TheJudgesystem.Web.ViewModels.Witnesses
{
    public class WitnessesListViewModel : PagingViewModel
    {

        public ICollection<CaseInList> Cases { get; set; }

    }
}
