using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TheJudgesystem.Data.Models;
using TheJudgesystem.Web.ViewModels.Defendants;

namespace TheJudgesystem.Services.Data.PeopleServices
{
    public interface IDefendantService
    {

        public IEnumerable<LawyerInList> GetLawyers(int page, int itemsPerPage = 6);

        public int GetCount();

        public Defendant GetDefendant(ClaimsPrincipal user);

        public bool HasLawyer(ClaimsPrincipal user);

        public MyLawyerViewModel GetMyLawyer(ClaimsPrincipal user);

        public MyCaseViewModel GetMyCase(ClaimsPrincipal user);

        public InfoViewModel GetInfo(ClaimsPrincipal user);

        public MyImageViewModel GetMyImage(ClaimsPrincipal user);

    }
}
