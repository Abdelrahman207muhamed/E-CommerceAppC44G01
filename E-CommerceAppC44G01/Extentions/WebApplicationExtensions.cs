using DomainLayer.Contracts;
using E_CommerceAppC44G01.CustomMiddelWare;
using Microsoft.EntityFrameworkCore.Internal;

namespace E_CommerceAppC44G01.Extentions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            //Create Object From Type That Implements IDbinitializer
            using var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();
            return app;
        }

        public static WebApplication UseCustomMiddleWareExceptions(this WebApplication app) 
        {
            app.UseMiddleware<CustomExceptionHandlerMiddelWare>();
            return app;
        }

    }
}
