using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : APIBaseController
    {
        #region login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(User);
        }
        #endregion

        #region register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(User);
        }

        #endregion
    }
}
