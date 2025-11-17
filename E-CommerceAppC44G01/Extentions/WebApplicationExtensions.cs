using DomainLayer.Contracts;
using E_CommerceAppC44G01.CustomMiddelWare;
using E_CommerceAppC44G01.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;

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
            await ObjectOfDataSeeding.IdentityDataSeed();
            return app;
        }

        public static WebApplication UseCustomMiddleWareExceptions(this WebApplication app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddelWare>();
            return app;
        }

        public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(  options =>
            {
                options.ConfigObject = new ConfigObject()
                {
                    DisplayRequestDuration = true

                };
                options.DocumentTitle = "My E-Commerce API";
                options.JsonSerializerOptions = new JsonSerializerOptions()
                {

                    PropertyNamingPolicy  = JsonNamingPolicy.CamelCase
                };
                options.DocExpansion(DocExpansion.None);
                options.EnableFilter();
                options.EnablePersistAuthorization();

            });

            return app;
        }
    }
}
