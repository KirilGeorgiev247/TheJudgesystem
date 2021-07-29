namespace TheJudgesystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TheJudgesystem.Data.Common.Models;

    public class Prison : BaseDeletableModel<int>
    {
        public Prison()
        {
            this.Cells = new HashSet<Cell>();
            this.Prisoners = new HashSet<Defendant>();
            this.Guards = new HashSet<Guard>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        public ICollection<Cell> Cells { get; set; }

        public ICollection<Defendant> Prisoners { get; set; }

        public ICollection<Guard> Guards { get; set; }
    }
}
