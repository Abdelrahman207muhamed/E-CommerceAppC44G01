using Service;
using Shared.Common;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_CommerceAppC44G01.Extentions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(cfg => { }, typeof(Persentation.AssemblyReference).Assembly);      //just empty class to recognize the assembly which the mapping profiles found.
            services.AddScoped<IServiceManager, ServiceManagerWithfactoryDelegate>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ICashService, CashService>();

            services.AddScoped<Func<IProductService>>(provider =>
                () => provider.GetRequiredService<IProductService>()
            );

            services.AddScoped<Func<IAuthenticationService>>(provider =>
                () => provider.GetRequiredService<IAuthenticationService>()
            );

            services.AddScoped<Func<IBasketService>>(provider =>
                () => provider.GetRequiredService<IBasketService>()
            );

            services.AddScoped<Func<IOrderService>>(provider =>
                () => provider.GetRequiredService<IOrderService>()
            );

            services.AddScoped<Func<IPaymentService>>(provider =>
                () => provider.GetRequiredService<IPaymentService>()
            );

            services.AddScoped<Func<ICashService>>(provider =>
            () => provider.GetRequiredService<ICashService>()
            );
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
            return services;
        }
    }
}
