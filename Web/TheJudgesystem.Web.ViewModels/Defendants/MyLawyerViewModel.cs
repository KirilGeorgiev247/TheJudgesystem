using System;
using System.Collections.Generic;
using System.Text;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Services.Mapping;

namespace TheJudgesystem.Web.ViewModels.Defendants
{
    public class MyLawyerViewModel : IMapFrom<Lawyer>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal? Rating { get; set; }

        public CvInList CV { get; set; }

        public string ImageUrl { get; set; }

        public decimal Salary { get; set; }

    }
}
