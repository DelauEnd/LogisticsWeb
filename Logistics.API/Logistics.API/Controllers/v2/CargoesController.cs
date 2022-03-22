using Logistics.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Logistics.API.Controllers.v2
{
    [ApiVersion("2")]
    [Route("api/{v:apiversion}/Cargoes")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class CargoesController : ControllerBase
    {
        public readonly ICargoService _cargoService;

        public CargoesController(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        /// <summary>
        /// Get list of cargoes
        /// </summary>
        /// <returns>Returns cargoes list</returns>
        /// <response code="400">If incorrect date filter</response>
        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetAllCargoes()
        {
            var cargoes = await _cargoService.GetAllCargoes();
            return Ok(cargoes);
        }
    }
}