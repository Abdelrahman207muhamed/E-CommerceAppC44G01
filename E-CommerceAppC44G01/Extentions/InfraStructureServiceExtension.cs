using DomainLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Data.DataSeed;
using Persistence.Repositories;

namespace E_CommerceAppC44G01.Extentions
{
    public static class InfraStructureServiceExtension
    {
        public static IServiceCollection AddInfraStructureService(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
           Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });
            //return
            return Services;

        }
    }
}
