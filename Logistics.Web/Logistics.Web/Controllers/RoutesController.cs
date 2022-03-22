using CargoTransportation.ActionFilters;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CargoTransportation.Controllers
{
    [Route("Routes")]
    [ServiceFilter(typeof(AuthenticatedAttribute))]
    public class RoutesController : ExtendedControllerBase
    {
        private static int RouteId { get; set; }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var response = await request.RouteRequestHandler.GetAllRoutes();

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            var routes = JsonConvert.DeserializeObject<IEnumerable<RouteDto>>(await response.Content.ReadAsStringAsync());
            return View(routes);
        }

        [HttpGet]
        [Route("Create")]
        [ServiceFilter(typeof(HasManagerRole))]
        public async Task<ActionResult> Create()
        {
            var transportResponse = await request.TransportRequestHandler.GetAllTransport();

            if (!transportResponse.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(transportResponse);

            var transport = JsonConvert.DeserializeObject<IEnumerable<TransportDto>>(await transportResponse.Content.ReadAsStringAsync());

            ViewBag.transport = transport;

            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(HasManagerRole))]
        public async Task<ActionResult> Create(RouteForCreationDto route)
        {
            HttpContent content = BuildHttpContent(route);
            var response = await request.RouteRequestHandler.CreateRoute(content);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{id}/Delete")]
        [ServiceFilter(typeof(HasManagerRole))]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await request.RouteRequestHandler.DeleteRouteById(id);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{id}/Edit")]
        [ServiceFilter(typeof(HasManagerRole))]
        public async Task<ActionResult> Edit(int id)
        {
            var response = await request.RouteRequestHandler.GetRouteById(id);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            var route = JsonConvert.DeserializeObject<RouteDto>(await response.Content.ReadAsStringAsync());

            var transportResponse = await request.TransportRequestHandler.GetAllTransport();
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
        [ServiceFilter(typeof(HasManagerRole))]
        public async Task<ActionResult> Edit(int id, RouteForUpdateDto route)
        {
            HttpContent content = BuildHttpContent(route);
            var response = await request.RouteRequestHandler.PutRouteById(id, content);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{id}/Cargoes")]
        [ServiceFilter(typeof(HasManagerRole))]
        public async Task<ActionResult> Cargoes(int id)
        {
            var response = await request.RouteRequestHandler.GetCargoesForRoute(id);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            RouteId = id;
            ViewBag.RouteToEdit = RouteId;

            var cargoes = JsonConvert.DeserializeObject<IEnumerable<CargoDto>>(await response.Content.ReadAsStringAsync());

            return View(cargoes);
        }

        [HttpGet]
        [Route("{routeId}/UnassignCargo")]
        [ServiceFilter(typeof(HasManagerRole))]
        public async Task<ActionResult> UnassignCargo(int routeId, int cargoId)
        {
            var jsonDiff = new JsonPatchDocument();
            jsonDiff.Remove("RouteId");

            HttpContent content = BuildHttpContent(jsonDiff);
            var response = await request.CargoRequestHandler.PatchCargoById(cargoId, content);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            return RedirectToAction("Cargoes", new { id = routeId });
        }

        [HttpGet]
        [Route("{id}/AssignCargoes")]
        [ServiceFilter(typeof(HasManagerRole))]
        public async Task<ActionResult> AssignCargoes(int id)
        {
            var response = await request.RouteRequestHandler.GetRouteById(id);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);
       
            var cargoesResponse = await request.CargoRequestHandler.GetUnassignedCargoes();
            var cargoes = JsonConvert.DeserializeObject<IEnumerable<CargoDto>>(await cargoesResponse.Content.ReadAsStringAsync());

            return View(cargoes);
        }

        [HttpPost]
        [Route("{id}/AssignCargoes")]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(HasManagerRole))]
        public async Task<ActionResult> AssignCargoes(int[] ids, int id)
        {
            var response = await request.RouteRequestHandler.AssignCargoesToRoute(id, ids);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            return RedirectToAction("Cargoes", new { id });
        }
    }
}