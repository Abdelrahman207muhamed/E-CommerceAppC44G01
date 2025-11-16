using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.Attributes
{
    public class Cashe(int DurationsInSec=90) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Create CasheKey

            string CashKey = CreateCashKey(context.HttpContext.Request);


            //Search For Value With Cash Key 

            ICashService cashService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();

            var CashValue = await cashService.GetAsync(CashKey);


            //Return Value If Not Null

            if(CashValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = CashValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK

                };
                return;
            }

            //InVoke Next

            var ExecutedContext = await next.Invoke();

            //Set Value With Cash Key

            if (ExecutedContext.Result is OkObjectResult result)
            {
                await cashService.SetAsync(CashKey, result, TimeSpan.FromSeconds(DurationsInSec));
            
            
            }

        }

        private string CreateCashKey(HttpRequest request)
        {

           //baseuUrl/api/products?typeid=10?brandId=
           StringBuilder Key = new StringBuilder();
            Key.Append(request.Path + '?');
            foreach (var Item in request.Query.OrderBy(Q => Q.Key))
            {
                Key.Append( $"{Item.Key}={Item.Value}&");
            }
            return Key.ToString();
        }
    }
}
