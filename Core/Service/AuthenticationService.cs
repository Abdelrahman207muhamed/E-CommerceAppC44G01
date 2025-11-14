using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager ,IConfiguration _Configuration) : IAuthenticationService
    {
        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {       
            //Check If Email Is Exists
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User is null)
            {
                throw new UserNotFoundException(loginDto.Email);
            }
            //Check Password 
            var IsPasswordValid = await _userManager.CheckPasswordAsync(User,loginDto.Password);
            if (IsPasswordValid)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await CreateTokenAsync(User)
                };
            }
            else 
            {
                throw  new UnauthorizedException();
            }
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            //Mapping Register Dto => Application User
            var User = new ApplicationUser()
            {

                Email = registerDto.Email,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.UserName
            };


            //Create User 
            var Result = await _userManager.CreateAsync(User, registerDto.Password);
            if (Result.Succeeded)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await CreateTokenAsync(User)
                };
            }
            else
            {
                var Errors = Result.Errors.Select(E => E.Description).ToList();
                throw new BadRequestException(Errors);

            }


        }
        private  async Task<string> CreateTokenAsync(ApplicationUser User)
        {

            var Claims = new List<Claim>()
            {
                new (ClaimTypes.Email,User.Email!),
                new (ClaimTypes.Name,User.UserName!),
                new (ClaimTypes.NameIdentifier,User.Id)

            };
            var Roles = await _userManager.GetRolesAsync(User);
            foreach (var role in Roles)
               
                Claims.Add(new Claim(ClaimTypes.Role, role));
            //---------------------------------
            var SecretKey = _Configuration.GetSection("JWTOptions")["SecretKey"];
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            //---------------------------------
            var Token = new JwtSecurityToken(
                issuer: _Configuration["JWTOptions.Issuer"],       //الجهة او السيرفر  اللي عملتلي التوكين
                audience: _Configuration["JWTOptions.Audience"], //دا الشخص المستخدم
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials:Creds


                );
            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}

