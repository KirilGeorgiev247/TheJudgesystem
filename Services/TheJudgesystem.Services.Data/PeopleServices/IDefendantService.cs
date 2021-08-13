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

        public Task<ICollection<LawyerInList>> GetLawyers(int page, int itemsPerPage = 4);

        public Task<int> GetCount();

        public Task<Defendant> GetDefendant(ClaimsPrincipal user);

        public Task<bool> HasLawyer(ClaimsPrincipal user);

        public Task<MyLawyerViewModel> GetMyLawyer(ClaimsPrincipal user);

        public Task<MyCaseViewModel> GetMyCase(ClaimsPrincipal user);

        public Task<InfoViewModel> GetInfo(ClaimsPrincipal user);

        public Task<MyImageViewModel> GetMyImage(ClaimsPrincipal user);

    }
}
