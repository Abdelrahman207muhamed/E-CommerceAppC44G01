using E_CommerceAppC44G01.Factories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;


namespace E_CommerceAppC44G01.Extentions
{
    public static class WebApiServicesExtensions
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            // using AddJsonOptions to handle the Enum values in the drop down of the Swagger.
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            var frontUrl = configuration.GetSection("URLS")["FrontUrl"];
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod()
                    .WithOrigins(frontUrl)
                    .AllowCredentials();
                });

            });
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();
         
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = APIResponseFactory.GenerateApiValidationErrorResponse;
            });

            return services;
        }
    }
}
