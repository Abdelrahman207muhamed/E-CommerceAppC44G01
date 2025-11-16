using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ErroModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ErrorToReturn), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ErrorToReturn), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationErrorToReturn), StatusCodes.Status400BadRequest)]

    public class APIBaseController:ControllerBase
    {
        protected string GetEmailFromToken() => User.FindFirstValue(ClaimTypes.Email);


    }
}
