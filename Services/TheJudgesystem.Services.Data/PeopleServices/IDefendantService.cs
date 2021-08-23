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

        public Task<ICollection<T>> GetLawyers<T>(int page, int itemsPerPage = 4);

        public Task<int> GetCount();

        public Task<Defendant> GetDefendant(ClaimsPrincipal user);

        public Task<bool> HasLawyer(ClaimsPrincipal user);

        public Task<T> GetMyLawyer<T>(ClaimsPrincipal user);

        public Task<T> GetMyCase<T>(ClaimsPrincipal user);

        public Task<InfoViewModel> GetInfo<T>(ClaimsPrincipal user);

        public Task<T> GetMyImage<T>(ClaimsPrincipal user);

    }
}
