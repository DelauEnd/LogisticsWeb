using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CargoTransportation.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Authorize]
        public ActionResult Login()
        {
            return Redirect(GetBaseUrl());
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookie");
            await HttpContext.SignOutAsync("oidc");

            var token = await HttpContext.GetTokenAsync("id_token");

            return Redirect(_configuration.GetSection("IdentityServerBaseUrl").Value + $"/connect/endsession?id_token_hint={token}&post_logout_redirect_uri={GetBaseUrl()}");
        }

        public ActionResult AddRole()
        {
            return Redirect(_configuration.GetSection("IdentityServerBaseUrl").Value + $"/account/addrole");
        }

        public IActionResult Registration()
        {
            return Redirect(_configuration.GetSection("IdentityServerBaseUrl").Value + $"/account/registration");
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