using DomainLayer.Exceptions;
using Shared.ErroModels;
using System.Text.Json;

namespace E_CommerceAppC44G01.CustomMiddelWare
{
    public class CustomExceptionHandlerMiddelWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddelWare> _logger;

        public CustomExceptionHandlerMiddelWare(RequestDelegate Next,ILogger<CustomExceptionHandlerMiddelWare>logger)
        {
            _next = Next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
                await HandelNotFoundEndPoint(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something Went Wrong");
                //Set Status Code For Response
                await HandelExceptionAsync(httpContext, ex);
            }
        }


        private static async Task HandelExceptionAsync(HttpContext httpContext, Exception ex)
        {
            var Response = new ErrorToReturn()
            {
                ErrorMessage = ex.Message
            };

            Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException=>StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException=>GetBadRequestErrors(badRequestException ,Response),
                _ => StatusCodes.Status500InternalServerError 
            };

            //Response Object As Json
            await httpContext.Response.WriteAsJsonAsync(Response);
            //var ResponseToReturn = JsonSerializer.Serialize(Response);
            //await httpContext.Response.WriteAsync(ResponseToReturn);
        }

        private static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandelNotFoundEndPoint(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"The EndPoint {httpContext.Request.Path} Is Not Found"
                };
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
