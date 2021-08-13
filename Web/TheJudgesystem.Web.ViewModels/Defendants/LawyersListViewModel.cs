namespace TheJudgesystem.Web.ViewModels.Defendants
{
    using System.Collections.Generic;

    public class LawyersListViewModel : PagingViewModel
    {
        public ICollection<LawyerInList> Lawyers { get; set; }
    }
}
