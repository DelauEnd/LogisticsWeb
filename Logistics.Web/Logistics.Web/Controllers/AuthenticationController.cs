using CargoTransportation.ActionFilters;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.ResponseDTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CargoTransportation.Controllers
{
    public class AuthenticationController : ExtendedControllerBase
    {
        [HttpGet]
        public ActionResult Login(string login, string password)
        {
            var user = new UserForAuthenticationDto()
            {
                Password = password,
                UserName = login
            };
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserForAuthenticationDto user)
        {
            try
            {
                HttpContent content = BuildHttpContent(user);
                var response = await request.AuthenticationRequestHandler.AuthenticateUser(content);

                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                var responseContent = JsonSerializer.Deserialize<AuthenticatedUserInfo>(await response.Content.ReadAsStringAsync());

                request.AuthenticationRequestHandler.InitUser(responseContent.AuthToken, responseContent.UserRoles);

                return RedirectToAction(nameof(Index), "Home");
            }
            catch
            {
                ViewBag.loginFailed = true;
                return View();
            }
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(UserForCreationDto user)
        {
            try
            {
                HttpContent content = BuildHttpContent(user);
                var response = await request.AuthenticationRequestHandler.CreateUser(content);

                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                var responseContent = JsonSerializer.Deserialize<AuthenticatedUserInfo>(await response.Content.ReadAsStringAsync());

                return RedirectToAction("Login", new { login = user.UserName, password = user.Password });
            }
            catch
            {
                ViewBag.registrationFailed = true;
                return View();
            }
        }

        [HttpGet]
        [ServiceFilter(typeof(HasAdministratorRole))]
        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(HasAdministratorRole))]
        public async Task<ActionResult> AddRole(Logistics.Models.WebModels.UserRole userRole)
        {
            try
            {
                var response = await request.AuthenticationRequestHandler.AddRole(userRole.UserName, userRole.Role);

                if (!response.IsSuccessStatusCode)
                    throw new Exception();

                var responseContent = JsonSerializer.Deserialize<AuthenticatedUserInfo>(await response.Content.ReadAsStringAsync());

                return RedirectToAction(nameof(Index), "Home");
            }
            catch
            {
                ViewBag.addingFailed = true;
                return View();
            }
        }
    }
}