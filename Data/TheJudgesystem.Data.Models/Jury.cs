namespace TheJudgesystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using TheJudgesystem.Data.Common.Models;

    public class Jury : BaseDeletableModel<int>
    {
        public Jury()
        {
            this.Members = new HashSet<Jurymember>();
        }

        public ICollection<Jurymember> Members { get; set; }

        public string Pronouncement { get; set; }

        public Case Case { get; set; }

        [ForeignKey(nameof(Case))]
        public int CaseId { get; set; }

    }
}
