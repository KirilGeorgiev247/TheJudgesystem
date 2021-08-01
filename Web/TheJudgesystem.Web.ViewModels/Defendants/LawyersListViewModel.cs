namespace TheJudgesystem.Web.ViewModels.Defendants
{
    using System.Collections.Generic;

    public class LawyersListViewModel : PagingViewModel
    {
        public IEnumerable<LawyerInList> Lawyers { get; set; }
    }
}
