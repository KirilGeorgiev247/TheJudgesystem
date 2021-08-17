using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheJudgesystem.Data;
using TheJudgesystem.Data.Common;
using TheJudgesystem.Data.Common.Repositories;
using TheJudgesystem.Data.Repositories;
using TheJudgesystem.Services.Data;
using TheJudgesystem.Services.Data.PeopleServices;
using TheJudgesystem.Services.Data.StuffServices;
using TheJudgesystem.Services.Messaging;

namespace TheJudgesystem.Web.Extensions
{
    public static class StartUpExtensions
    {

        public static void RegisterDependecies(this IServiceCollection services)
        {
            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IDefendantService, DefendantService>();
            services.AddTransient<IJudgesService, JudgesService>();
            services.AddTransient<IJurymembersService, JurymembersService>();
            services.AddTransient<ILawyersService, LawyersService>();
            services.AddTransient<IProsecutorsService, ProsecutorsService>();
            services.AddTransient<IGuardsService, GuardsService>();
            services.AddTransient<IWitnessesService, WitnessesService>();
            services.AddTransient<ICasesService, CasesService>();

        }

    }
}
