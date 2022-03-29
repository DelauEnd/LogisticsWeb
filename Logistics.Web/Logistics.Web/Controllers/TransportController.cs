using CargoTransportation.Utils;
using Logistics.Models.Enums;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Logistics.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RequestHandler.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CargoTransportation.Controllers
{
    [Authorize]
    [Route("Transport")]
    public class TransportController : Controller
    {
        private static TransportForUpdateDto TransportToUpdate { get; set; }

        private readonly ITransportRequestHandler _transportRequestHandler;
        public TransportController(ITransportRequestHandler transportRequestHandler)
        {
            _transportRequestHandler = transportRequestHandler;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var response = await _transportRequestHandler.GetAllTransport();

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            var customers = JsonConvert.DeserializeObject<IEnumerable<TransportDto>>(await response.Content.ReadAsStringAsync());
            return View(customers);
        }

        [HttpGet]
        [Route("Create")]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<ActionResult> Create(TransportForCreationDto transport)
        {
            HttpContent content = HttpContentBuilder.BuildContent(transport);
            var response = await _transportRequestHandler.CreateTransport(content);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{id}/Delete")]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _transportRequestHandler.DeleteTransportById(id);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{id}/Edit")]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<ActionResult> Edit(int id)
        {
            var response = await _transportRequestHandler.GetTransportById(id);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            var transport = JsonConvert.DeserializeObject<TransportDto>(await response.Content.ReadAsStringAsync());

            TransportToUpdate = new TransportForUpdateDto
            {
                RegistrationNumber = transport.RegistrationNumber,
                LoadCapacity = transport.LoadCapacity,
                Driver = transport.Driver
            };

            return View(TransportToUpdate);
        }

        [HttpPost]
        [Route("{id}/Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<ActionResult> Edit(int id, TransportForUpdateDto transport)
        {
            var jsonPatch = JsonPatcher.CreatePatch(TransportToUpdate, transport);

            HttpContent content = HttpContentBuilder.BuildContent(jsonPatch);
            var response = await _transportRequestHandler.PatchTransportById(id, content);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{id}/Details")]
        public async Task<ActionResult> Details(int id)
        {
            var response = await _transportRequestHandler.GetTransportById(id);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            var transport = JsonConvert.DeserializeObject<TransportDto>(await response.Content.ReadAsStringAsync());

            return View(transport.Driver);
        }
    }
}