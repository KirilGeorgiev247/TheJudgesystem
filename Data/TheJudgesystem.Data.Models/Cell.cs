namespace TheJudgesystem.Data.Models
{
    using System.Collections.Generic;

    using TheJudgesystem.Data.Common.Models;

    public class Cell : BaseDeletableModel<int>
    {
        public Cell()
        {
            this.Prisoners = new HashSet<Defendant>();
        }

        public int Capacity => 2;

        public ICollection<Defendant> Prisoners { get; set; }

    }
}
