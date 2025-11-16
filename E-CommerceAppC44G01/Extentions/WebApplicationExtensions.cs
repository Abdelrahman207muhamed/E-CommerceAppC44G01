using DomainLayer.Contracts;
using E_CommerceAppC44G01.CustomMiddelWare;

namespace E_CommerceAppC44G01.Extentions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDatabaseAsync(this WebApplication app)
        {
            #region Call SeedData before Any Request.
            // to get an instance of DataSeeding Manually and call the method SeedData before the request executed.
            using var scope = app.Services.CreateScope();
            var objOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await objOfDataSeeding.DataSeedAsync();
            await objOfDataSeeding.IdentityDataSeed();
            #endregion
            return app;
        }
        public static WebApplication UseExceptionHandlingMiddleWare(this WebApplication app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddelWare>();
            return app;
        }

        public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseSwaggerUI(options =>
            //{
            //    options.ConfigObject = new Swashbuckle.AspNetCore.SwaggerUI.ConfigObject()
            //    {
            //        DisplayRequestDuration = true
            //    };
            //    options.DocumentTitle = "My E-Commerce API";
            //    options.JsonSerializerOptions = new JsonSerializerOptions()
            //    {
            //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //    };
            //    options.DocExpansion(DocExpansion.None);
            //    options.EnableFilter();
            //    options.EnablePersistAuthorization();
            //});
            return app;
        }
    }
}
