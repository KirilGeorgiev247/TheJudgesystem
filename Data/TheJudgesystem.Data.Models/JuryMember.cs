namespace TheJudgesystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TheJudgesystem.Data.Common.Enumerations;
    using TheJudgesystem.Data.Common.Models;

    public class Jurymember : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public Opinion Opinion { get; set; }

        [ForeignKey(nameof(Opinion))]
        public int? OpinionId { get; set; }

        public Jury Jury { get; set; }

        [ForeignKey(nameof(Jury))]
        public int? JuryId { get; set; }
    }
}
