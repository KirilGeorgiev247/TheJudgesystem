namespace TheJudgesystem.Web.ViewModels.Defendants
{
    using TheJudgesystem.Data.Models;
    using TheJudgesystem.Services.Mapping;

    public class CvInList : IMapFrom<CV>
    {
        public int Age { get; set; }

        public string BirthTown { get; set; }

        public string School { get; set; }

        public string University { get; set; }

        public string Achievements { get; set; }
    }
}
