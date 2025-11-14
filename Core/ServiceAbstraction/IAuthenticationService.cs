using Shared.Dtos.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        Task<bool> CheckEmailAsync(string Email);
        Task<AddressDto> GetCurrentUserAddress(string Email);
        Task<AddressDto> UpdateCurrentUserAddress(AddressDto addressDto, string Email);
        Task<UserDto> GetCurrentUser(string Email);

    }
}
