using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheJudgesystem.Web.ViewModels.Roles;

namespace TheJudgesystem.Services.Data
{
    public interface IRolesService
    {

        public Task AddLawyer(LawyerInputModel lawyer);


    }
}
