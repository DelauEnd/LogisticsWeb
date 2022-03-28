using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RequestHandler.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CargoTransportation.Controllers
{
    // [ServiceFilter(typeof(AuthenticatedAttribute))]
    [Authorize]
    public class CargoesController : Controller
    {
        private static CargoForUpdateDto CargoToUpdate { get; set; }

        private readonly ICargoRequestHandler _cargoesHandler;
        public CargoesController(ICargoRequestHandler cargoesHandler)
        {
            _cargoesHandler = cargoesHandler;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var response = await _cargoesHandler.GetAllCargoes();

            var cargoes = JsonConvert.DeserializeObject<IEnumerable<CargoDto>>(await response.Content.ReadAsStringAsync());
            return View(cargoes);
        }
    }
}