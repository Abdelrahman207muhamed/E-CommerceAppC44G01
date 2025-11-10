using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Data.DataSeed;
using Persistence.Repositories;

namespace E_CommerceAppC44G01.Extentions
{
    public static class InfraStructureServiceExtension
    {
        public static IServiceCollection AddInfraStructureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });

            services.AddScoped<IDataSeeding, DataSeeding>();

            services.AddDbContext<StoreIdentityDbContext>(options =>
           {
               options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));

           });



            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddIdentityCore<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();


            return services;

        }
    }
}
