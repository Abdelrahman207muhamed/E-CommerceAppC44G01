using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : APIBaseController
    {
        #region login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
         {
            var User = await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(User);
        }
        #endregion

        #region register
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var User = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(User);
        }

        #endregion

        #region Check Email 
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmail(string Email)
        {
            var Result = await _serviceManager.AuthenticationService.CheckEmailAsync(Email);
            return Ok(Result);

        }

        #endregion

        #region CurrentUser 
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<UserDto>> GetCurrentUser() 
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var AppUser = await _serviceManager.AuthenticationService.GetCurrentUser(Email);
            return Ok(AppUser);

        }

        #endregion

       
        #region UpdateCurrentUserAddress 
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto addressDto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var UpdatedAddress = await _serviceManager.AuthenticationService.UpdateCurrentUserAddress(addressDto, Email);
            return Ok(UpdatedAddress);

        }



        #endregion

    }
}
