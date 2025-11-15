using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Data.DataSeed;
using Persistence.Repositories;
using StackExchange.Redis; 

namespace E_CommerceAppC44G01.Extentions
{
    public static class InfraStructureServiceExtension
    {
        public static IServiceCollection AddInfraStructureService(this IServiceCollection services, IConfiguration configuration)
        {
            //  SQL Server (Main Store DB)
            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //  SQL Server (Identity DB)
            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            //  Redis Connection Registration
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var redisConfig = ConfigurationOptions.Parse(
                    configuration.GetConnectionString("Redis")!, true);
                return ConnectionMultiplexer.Connect(redisConfig);
            });

            //Basket Repository
            services.AddScoped<IBasketRepository, BasketRepository>();

            //  Data Seeding
            services.AddScoped<IDataSeeding, DataSeeding>();

            //  Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //  Identity Core Setup
            services.AddIdentityCore<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;
        }
    }
}
