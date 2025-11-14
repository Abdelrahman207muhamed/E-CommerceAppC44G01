using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class AuthenticationService(UserManager<ApplicationUser> _userManager ,IConfiguration _Configuration , IMapper _mapper) : IAuthenticationService
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
                issuer: _Configuration["JWTOptions:Issuer"],       //الجهة او السيرفر  اللي عملتلي التوكين
                audience: _Configuration["JWTOptions:Audience"], //دا الشخص المستخدم
                claims: Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials:Creds


                );
            return new JwtSecurityTokenHandler().WriteToken(Token);




        }

        public async Task<bool> CheckEmailAsync(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email);
            return User is not null;
        }

        public async Task<UserDto> GetCurrentUser(string Email)
        {
            var User = await _userManager.FindByEmailAsync(Email) ?? throw new UserNotFoundException(Email);
            return new UserDto()
            {
                Email = User.Email,
                DisplayName = User.DisplayName,
                Token = await CreateTokenAsync(User)
            };
        }

        public async Task<AddressDto> GetCurrentUserAddress(string Email)
        {
            var User = await _userManager.Users.Include(u=>u.Address )
                .FirstOrDefaultAsync(u=>u.Email==Email)
                ?? throw new UserNotFoundException(Email);

            if (User.Address is not null)
            {
                return _mapper.Map<Address, AddressDto>(User.Address);
            }
            else
            {
                throw new AddressNotFoundException(User.UserName);
            }

        }

        public async Task<AddressDto> UpdateCurrentUserAddress(AddressDto addressDto, string Email)
        {
            var User = await _userManager.Users.Include(u => u.Address)
                  .FirstOrDefaultAsync(u => u.Email == Email)
                  ?? throw new UserNotFoundException(Email);

            if (User.Address is not null) //Update
            {
                User.Address.FirstName = addressDto.FirstName;
                User.Address.LastName = addressDto.LastName;
                User.Address.City = addressDto.City;
                User.Address.Country = addressDto.Country;
                User.Address.Street = addressDto.Street;

            }
            else 
            {
                //Add New Address

                User.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }

            await _userManager.UpdateAsync(User);
            return _mapper.Map<AddressDto>(User.Address);
        }
    }
}

