using E_CommerceAppC44G01.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAppC44G01.Extentions
{
    public  static class WebApiServices
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection Services)
        {
            Services.AddControllers();
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = APIResponseFactory.GenerateApiValidationErrorResponse;
            });
            return Services;
        }
    }
}
