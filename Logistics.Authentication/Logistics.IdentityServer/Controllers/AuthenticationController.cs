using IdentityModel.Client;
using IdentityServer4;
using Logistics.IdentityServer.Services.Interfaces;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.ResponseDTO;
using Logistics.Models.WebModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Logistics.IdentityServer.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly Services.Interfaces.IAccountService _authenticationService;

        public AuthenticationController(IAccountService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="userForCreation"></param>
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForCreationDto userForCreation)
        {
            await _authenticationService.CreateUser(userForCreation);
            return Ok();
        }

        /// <summary>
        /// Add role to user
        /// | Required role: Administrator
        /// </summary>
        /// <param name="login"></param>
        /// <param name="role"></param>
        [HttpPost]
        [Route("AddRole")]
        [Authorize]
        public async Task<IActionResult> AddRoleToUser([FromQuery] string login, [FromQuery] string role)
        {
            await _authenticationService.AddRoleToUser(login, role);
            return Ok();
        }

        /// <summary>
        /// Authenticate user by username and password
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns access token for authenticated user</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            var userInfo = await _authenticationService.AuthenticateUser(user);
            return Ok(userInfo);
        }
    }
}
