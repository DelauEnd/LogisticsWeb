using Logistics.Models.Enums;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Logistics.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RequestHandler.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CargoTransportation.Controllers
{
    [Authorize]
    [Route("Routes")]
    public class RoutesController : Controller
    {
        private static int RouteId { get; set; }

        private readonly IRouteRequestHandler _routeRequestHandler;
        private readonly ICargoRequestHandler _cargoRequestHandler;
        private readonly ITransportRequestHandler _transportRequestHandler;
        public RoutesController(IRouteRequestHandler routeRequestHandler, ICargoRequestHandler cargoRequestHandler, ITransportRequestHandler transportRequestHandler)
        {
            _routeRequestHandler = routeRequestHandler;
            _cargoRequestHandler = cargoRequestHandler;
            _transportRequestHandler = transportRequestHandler;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var response = await _routeRequestHandler.GetAllRoutes();

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            var routes = JsonConvert.DeserializeObject<IEnumerable<RouteDto>>(await response.Content.ReadAsStringAsync());
            return View(routes);
        }

        [HttpGet]
        [Route("Create")]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> Create()
        {
            var transportResponse = await _transportRequestHandler.GetAllTransport();

            if (!transportResponse.IsSuccessStatusCode)
                return new StatusCodeResult((int)transportResponse.StatusCode);

            var transport = JsonConvert.DeserializeObject<IEnumerable<TransportDto>>(await transportResponse.Content.ReadAsStringAsync());

            ViewBag.transport = transport;

            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> Create(RouteForCreationDto route)
        {
            HttpContent content = HttpContentBuilder.BuildContent(route);
            var response = await _routeRequestHandler.CreateRoute(content);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{id}/Delete")]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _routeRequestHandler.DeleteRouteById(id);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{id}/Edit")]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> Edit(int id)
        {
            var response = await _routeRequestHandler.GetRouteById(id);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            var route = JsonConvert.DeserializeObject<RouteDto>(await response.Content.ReadAsStringAsync());

            var transportResponse = await _transportRequestHandler.GetAllTransport();
            var transport = JsonConvert.DeserializeObject<IEnumerable<TransportDto>>(await transportResponse.Content.ReadAsStringAsync());

            ViewBag.transport = transport;

            var routeToUpdate = new RouteForUpdateDto
            {
                TransportRegistrationNumber = route.TransportRegistrationNumber
            };

            return View(routeToUpdate);
        }

        [HttpPost]
        [Route("{id}/Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> Edit(int id, RouteForUpdateDto route)
        {
            HttpContent content = HttpContentBuilder.BuildContent(route);
            var response = await _routeRequestHandler.PutRouteById(id, content);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{id}/Cargoes")]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> Cargoes(int id)
        {
            var response = await _routeRequestHandler.GetCargoesForRoute(id);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            RouteId = id;
            ViewBag.RouteToEdit = RouteId;

            var cargoes = JsonConvert.DeserializeObject<IEnumerable<CargoDto>>(await response.Content.ReadAsStringAsync());

            return View(cargoes);
        }

        [HttpGet]
        [Route("{routeId}/UnassignCargo")]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> UnassignCargo(int routeId, int cargoId)
        {
            var jsonDiff = new JsonPatchDocument();
            jsonDiff.Remove("RouteId");

            HttpContent content = HttpContentBuilder.BuildContent(jsonDiff);
            var response = await _cargoRequestHandler.PatchCargoById(cargoId, content);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction("Cargoes", new { id = routeId });
        }

        [HttpGet]
        [Route("{id}/AssignCargoes")]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> AssignCargoes(int id)
        {
            var response = await _routeRequestHandler.GetRouteById(id);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            var cargoesResponse = await _cargoRequestHandler.GetUnassignedCargoes();
            var cargoes = JsonConvert.DeserializeObject<IEnumerable<CargoDto>>(await cargoesResponse.Content.ReadAsStringAsync());

            return View(cargoes);
        }

        [HttpPost]
        [Route("{id}/AssignCargoes")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> AssignCargoes(int[] ids, int id)
        {
            HttpContent content = HttpContentBuilder.BuildContent(ids.ToList());
            var response = await _cargoRequestHandler.AssignCargoesToRoute(id, content);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction("Cargoes", new { id });
        }
    }
}