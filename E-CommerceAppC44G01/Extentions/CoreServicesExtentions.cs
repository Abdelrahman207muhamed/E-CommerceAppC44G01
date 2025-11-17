using DomainLayer.Contracts;
using Persistence.Repositories;
using Service;
using ServiceAbstraction;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace E_CommerceAppC44G01.Extentions
{
    public static class CoreServicesExtentions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            Services.AddScoped<IServiceManager, ServiceManager>();

            return Services;
        }


    }
}
