using Logistics.IdentityServer.Entities.Models;
using Logistics.IdentityServer.Services.Interfaces;
using Logistics.Models.Enums;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.ResponseDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Logistics.IdentityServer.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly SignInManager<User> _signInManager;
        private static string ReturnUrl { get; set; }

        public AuthenticationController(IAuthenticationService authenticationService,
            SignInManager<User> signInManager)
        {
            _authenticationService = authenticationService;
            _signInManager = signInManager;
        }   

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {

            ReturnUrl ??= returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForAuthenticationDto authUser)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(authUser.UserName, authUser.Password, false, false);
            if (signInResult.Succeeded)
            {
                return Redirect(ReturnUrl);
            }
            else
            {
                ViewBag.loginFailed = true;
                return View();
            }           
        }

    }
}
