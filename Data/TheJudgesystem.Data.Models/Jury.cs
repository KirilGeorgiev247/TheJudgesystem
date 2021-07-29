namespace TheJudgesystem.Data.Models
{
    using System.Collections.Generic;

    using TheJudgesystem.Data.Common.Models;

    public class Jury : BaseDeletableModel<int>
    {
        public Jury()
        {
            this.Members = new HashSet<JuryMember>();
        }

        public ICollection<JuryMember> Members { get; set; }

        public string Pronouncement { get; set; }

    }
}
