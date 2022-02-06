using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FIADbContext.Repositories;

namespace FIAWebApi.BL
{
    public static class ServiceCollectionDataRepositoryExtension
    {
        public static void AddFIARepositories(this IServiceCollection services)
        {
            services.AddTransient<EnterpriseRepository>();
            services.AddTransient<FinancialResultRepository>();
        }

        public static void AddFIAServices(this IServiceCollection services)
        {
            services.AddTransient<EnterprisesService>();
            services.AddScoped<UsersService>();
        }

    }
}
