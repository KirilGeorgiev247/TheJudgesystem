namespace TheJudgesystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TheJudgesystem.Data.Common.Models;

    public class Cell : BaseDeletableModel<int>
    {
        public Cell()
        {
            this.Prisoners = new HashSet<Defendant>();
        }

        public int Capacity => 2;

        public ICollection<Defendant> Prisoners { get; set; }

        [Required]
        public Prison Prison { get; set; }

        [ForeignKey(nameof(Prison))]
        public int PrisonId { get; set; }

    }
}
