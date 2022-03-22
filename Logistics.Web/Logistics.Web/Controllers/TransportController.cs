using CargoTransportation.ActionFilters;
using CargoTransportation.Utils;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CargoTransportation.Controllers
{
    [Route("Transport")]
    [ServiceFilter(typeof(AuthenticatedAttribute))]
    public class TransportController : ExtendedControllerBase
    {
        private static TransportForUpdateDto TransportToUpdate { get; set; }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var response = await request.TransportRequestHandler.GetAllTransport();

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            var customers = JsonConvert.DeserializeObject<IEnumerable<TransportDto>>(await response.Content.ReadAsStringAsync());
            return View(customers);
        }

        [HttpGet]
        [Route("Create")]
        [ServiceFilter(typeof(HasAdministratorRole))]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(HasAdministratorRole))]
        public async Task<ActionResult> Create(TransportForCreationDto transport)
        {
            HttpContent content = BuildHttpContent(transport);
            var response = await request.TransportRequestHandler.CreateTransport(content);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{id}/Delete")]
        [ServiceFilter(typeof(HasAdministratorRole))]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await request.TransportRequestHandler.DeleteTransportById(id);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{id}/Edit")]
        [ServiceFilter(typeof(HasAdministratorRole))]
        public async Task<ActionResult> Edit(int id)
        {
            var response = await request.TransportRequestHandler.GetTransportById(id);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

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
        [ServiceFilter(typeof(HasAdministratorRole))]
        public async Task<ActionResult> Edit(int id, TransportForUpdateDto transport)
        {
            var jsonPatch = JsonPatcher.CreatePatch(TransportToUpdate, transport);

            HttpContent content = BuildHttpContent(jsonPatch);
            var response = await request.TransportRequestHandler.PatchTransportById(id, content);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{id}/Details")]
        public async Task<ActionResult> Details(int id)
        {
            var response = await request.TransportRequestHandler.GetTransportById(id);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            var transport = JsonConvert.DeserializeObject<TransportDto>(await response.Content.ReadAsStringAsync());

            return View(transport.Driver);
        }
    }
}