namespace TheJudgesystem.Web.ViewModels.Defendants
{
    using AutoMapper;
    using TheJudgesystem.Data.Models;
    using TheJudgesystem.Services.Mapping;

    // IHaveCustomMappings
    public class LawyerInList : IMapFrom<Lawyer>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal? Rating { get; set; }

        public CvInList CV { get; set; }

        public string ImageUrl { get; set; }

        public decimal Salary { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<Lawyer, LawyerInList>()
        //        .ForMember(x=> x.ImageUrl, opt => 
        //        opt.MapFrom(r=> ...)
        //}
    }
}
