using Service;
using ServiceAbstraction;
using System.Runtime.CompilerServices;

namespace E_CommerceAppC44G01.Extentions
{
    public static class CoreServicesExtentions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddAutoMapper(X => X.AddProfile(new MappingProfiles()));
            return Services;
        }
    }
}
