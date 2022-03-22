using AutoMapper;
using Logistics.Entities.Models;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.ResponseDTO;
using Logistics.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Logistics.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IMapper _mapper;

        public AuthenticationService(UserManager<User> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager, IAuthenticationManager authManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authManager = authManager;
            _mapper = mapper;
        }

        public async Task AddRoleToUser(string login, string role)
        {
            var user = await _userManager.FindByNameAsync(login);

            if (user == null)
                throw new Exception("User not found");

            if (!await _roleManager.RoleExistsAsync(role))
                throw new Exception("Role not exists");

            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<AuthenticatedUserInfo> AuthenticateUser(UserForAuthenticationDto user)
        {
            var validUser = await _authManager.ReturnUserIfValid(user);

            if (validUser == null)
            {
                throw new Exception("Unauthorized");
            }

            return new AuthenticatedUserInfo
            {
                AuthToken = await _authManager.CreateToken(validUser),
                UserRoles = await _userManager.GetRolesAsync(validUser)
            };
        }

        public async Task CreateUser(UserForCreationDto userForCreation)
        {
            var user = _mapper.Map<User>(userForCreation);

            var result = await _userManager.CreateAsync(user, userForCreation.Password);

            if (!result.Succeeded)
            {
                var errors = "";
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Code}: {error.Description}\n";
                }
                throw new Exception(errors);
            }
        }
    }
}
