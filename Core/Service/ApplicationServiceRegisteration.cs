using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ApplicationServiceRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(X => X.AddProfiles(new MappingProfiles)));
            Services.AddScoped<IServiceManager, ServiceManagerWithfactoryDelegate>();


            return Services;
        }


    }
}
