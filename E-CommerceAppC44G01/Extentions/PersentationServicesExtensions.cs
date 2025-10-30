using E_CommerceAppC44G01.Factories;
using Microsoft.AspNetCore.Mvc;
using Service;
using ServiceAbstraction;

namespace E_CommerceAppC44G01.Extentions
{
    public static class PersentationServicesExtensions
    {
        public static IServiceCollection AddPersentationServices(this IServiceCollection Services)
        {
            Services.AddControllers().AddApplicationPart(typeof(Persentation.AssemblyReference).Assembly);
            Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = APIResponseFactory.GenerateApiValidationErrorResponse;
            });
            return Services;
        }
    }
}