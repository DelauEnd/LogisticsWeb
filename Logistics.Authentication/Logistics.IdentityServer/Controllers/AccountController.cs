using IdentityServer4;
using Logistics.IdentityServer.Services.Interfaces;
using Logistics.Models.IdentityModels;
using Logistics.Models.RequestDTO;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.WebModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Logistics.IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        private string ReturnUrl
        {
            get
            {
                return _returnUrl ?? _configuration.GetSection("MVCBaseUrl").Value + "/authentication/login";
            }
        }
        private string _returnUrl;

        public AccountController(IAccountService accountService,
            SignInManager<User> signInManager, IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _accountService = accountService;
            _signInManager = signInManager;
            _configuration = configuration;
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> Logout(string returnUrl)
        {
            _returnUrl = returnUrl;
            await HttpContext.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            await HttpContext.SignOutAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme);
            return Redirect(ReturnUrl);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            _returnUrl = returnUrl;
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

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserForCreationDto userForCreation)
        {
            try
            {
                await _accountService.CreateUser(userForCreation);
            }
            catch
            {
                ViewBag.registrationFailed = true;
                return View();
            }
            return Redirect(ReturnUrl);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRole(AddRoleDto roleToAdd)
        {
            try
            {
                await _accountService.AddRoleToUser(roleToAdd.UserName, roleToAdd.Role);
            }
            catch
            {
                ViewBag.addingFailed = true;
                return View();
            }
            return Redirect(ReturnUrl);
        }

        public string GetBaseUrl()
        {
            var request = HttpContext.Request;

            var host = request.Host.ToUriComponent();

            var pathBase = request.PathBase.ToUriComponent();

            return $"{request.Scheme}://{host}{pathBase}";
        }
    }
}
