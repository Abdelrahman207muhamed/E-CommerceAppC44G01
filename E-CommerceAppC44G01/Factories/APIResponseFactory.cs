using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.ErroModels;

namespace E_CommerceAppC44G01.Factories
{
    public class APIResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext Context)
        {
        
            
                var Errors = Context.ModelState.Where(M => M.Value?.Errors.Any()==true)
                                    .Select(M => new ValidationErrors()
                                    {
                                        Filed = M.Key,
                                        Errors = M.Value?.Errors.Select(E => E.ErrorMessage)??new List<string>()
                                    });
                var Response = new ValidationErrorToReturn()
                {

                    ValidationErrors = Errors,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "One or more Validation Errors Occured"
                };
                return new BadRequestObjectResult(Response);

            
        }
    }
}
