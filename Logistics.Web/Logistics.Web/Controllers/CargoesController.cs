using CargoTransportation.Utils;
using Logistics.Models.Enums;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Logistics.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RequestHandler.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CargoTransportation.Controllers
{
    [Authorize]
    [Route("Cargoes")]
    public class CargoesController : Controller
    {
        private static CargoForUpdateDto CargoToUpdate { get; set; }

        private readonly ICargoRequestHandler _cargoesHandler;
        private readonly ICargoCategoriesRequestHandler _categoriesHandler;
        private readonly IOrderRequestHandler _ordersHandler;
        public CargoesController(ICargoRequestHandler cargoesHandler, ICargoCategoriesRequestHandler categoriesHandler, IOrderRequestHandler ordersHandler)
        {
            _cargoesHandler = cargoesHandler;
            _categoriesHandler = categoriesHandler;
            _ordersHandler = ordersHandler;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var response = await _cargoesHandler.GetAllCargoes();

            var cargoes = JsonConvert.DeserializeObject<IEnumerable<CargoDto>>(await response.Content.ReadAsStringAsync());
            return View(cargoes);
        }

        [HttpGet]
        [Route("Cargoes/{id}/Details")]
        public async Task<ActionResult> Details(int id)
        {
            var response = await _cargoesHandler.GetCargoById(id);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            var cargo = JsonConvert.DeserializeObject<CargoDto>(await response.Content.ReadAsStringAsync());

            ViewBag.Image = cargo.Image;
            return View(cargo.Dimensions);
        }

        [HttpGet]
        [Authorize(Roles = nameof(UserRole.Manager))]
        [Route("Orders/{orderId}/CreateCargo")]
        public async Task<ActionResult> CreateCargo(int orderId)
        {
            var categoriesResponse = await _categoriesHandler.GetAllCategories();

            if (!categoriesResponse.IsSuccessStatusCode)
                return new StatusCodeResult((int)categoriesResponse.StatusCode);

            var categories = JsonConvert.DeserializeObject<IEnumerable<CargoCategoryDto>>(await categoriesResponse.Content.ReadAsStringAsync());

            ViewBag.categories = categories;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Manager))]
        [ValidateAntiForgeryToken]
        [Route("Orders/{orderId}/CreateCargo")]
        public async Task<ActionResult> CreateCargo(CargoForCreationDto cargo, int orderId, [FromForm]IFormFile addedImage)
        {
            var image = addedImage != null ? BuildImageBytes(addedImage) : new byte[0];
            cargo.Image = image;

            var cargoToAdd = new List<CargoForCreationDto>() { cargo };

            HttpContent content = HttpContentBuilder.BuildContent(cargoToAdd);
            var response = await _ordersHandler.CreateCargoForOrder(orderId, content);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction(nameof(Index));
        }

        private byte[] BuildImageBytes(IFormFile image)
        {
            using var binaryReader = new BinaryReader(image.OpenReadStream());
            return binaryReader.ReadBytes((int)image.Length);
        }

        [HttpGet]
        [Authorize(Roles = nameof(UserRole.Manager))]
        [Route("Cargoes/{id}/Edit")]
        public async Task<ActionResult> Edit(int id)
        {
            var response = await _cargoesHandler.GetCargoById(id);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            var cargo = JsonConvert.DeserializeObject<CargoDto>(await response.Content.ReadAsStringAsync());

            var categoriesResponse = await _categoriesHandler.GetAllCategories();
            var categories = JsonConvert.DeserializeObject<IEnumerable<CargoCategoryDto>>(await categoriesResponse.Content.ReadAsStringAsync());

            ViewBag.categories = categories;


            CargoToUpdate = new CargoForUpdateDto
            {
                Title = cargo.Title,
                ArrivalDate = cargo.ArrivalDate,
                DepartureDate = cargo.DepartureDate,
                CategoryId = categories.Where(categ => categ.Title == cargo.Category).FirstOrDefault().Id,
                Weight = cargo.Weight,
                Dimensions = cargo.Dimensions,
            };

            return View(CargoToUpdate);
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Manager))]
        [ValidateAntiForgeryToken]
        [Route("Cargoes/{id}/Edit")]
        public async Task<ActionResult> Edit(int id, CargoForUpdateDto cargo, [FromForm]IFormFile addedImage)
        {
            var image = addedImage != null ? BuildImageBytes(addedImage) : CargoToUpdate.Image;
            cargo.Image = image;

            var jsonPatch = JsonPatcher.CreatePatch(CargoToUpdate, cargo);

            HttpContent content = HttpContentBuilder.BuildContent(jsonPatch);
            var response = await _cargoesHandler.PatchCargoById(id, content);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = nameof(UserRole.Manager))]
        [Route("Cargoes/{id}/Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _cargoesHandler.DeleteCargoById(id);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction("Index");
        }
    }
}