using Logistics.Models.Enums;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.OrderService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.OrderService.Controllers
{
    [Route("api/Cargoes"), Authorize]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CargoesController : ControllerBase
    {
        private readonly ICargoService _cargoService;

        public CargoesController(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        /// <summary>
        /// Get list of cargo categories
        /// </summary>
        /// <returns>Returns cargo list</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCargoes()
        {
            return Ok(await _cargoService.GetAllCargoes());
        }

        /// <summary>
        /// Get list of cargo categories
        /// </summary>
        /// <returns>Returns unassigned cargoes</returns>
        [HttpGet]
        [Route("Unassigned")]
        public async Task<IActionResult> GetUnassignedCargoes()
        {
            return Ok(await _cargoService.GetUnassignedCargoes());
        }

        /// <summary>
        /// Get cargoes by requested route id
        /// </summary>
        /// <param name="routeId"></param>
        /// <returns>Returns cargoes by requested order id</returns>
        [HttpGet("OnRoute/{routeId}")]
        public async Task<IActionResult> GetCargoesByRouteId(int routeId)
        {
            return Ok(await _cargoService.GetCargoesByRouteId(routeId));
        }

        /// <summary>
        /// Get cargo by id
        /// </summary>
        /// <param name="cargoId"></param>
        /// <returns>Returns requested cargo</returns>
        [HttpGet("{cargoId}")]
        public async Task<IActionResult> GetCargoById(int cargoId)
        {
            return Ok(await _cargoService.GetCargoById(cargoId));
        }

        /// <summary>
        /// Delete cargo by id
        /// | Required role: Manager
        /// </summary>
        /// <param name="cargoId"></param>
        [HttpDelete("{cargoId}"), Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> DeleteCargoById(int cargoId)
        {
            await _cargoService.DeleteCargoById(cargoId);
            return Ok();
        }

        /// <summary>
        /// Update cargo by id
        /// | Required role: Manager
        /// </summary>
        /// <param name="cargoId"></param>
        /// <param name="patchDoc"></param>
        /// <returns>Returns updated cargo</returns>
        [HttpPatch("{cargoId}"), Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> PartiallyUpdateCargoById(int cargoId, [FromBody] JsonPatchDocument<CargoForUpdateDto> patchDoc)
        {
            return Ok(await _cargoService.PatchCargoById(cargoId, patchDoc));
        }

        /// <summary>
        /// Mark cargo by requested id to route by requested id
        /// | Required role: Manager
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="routeId"></param>
        [HttpPost("AssignToRoute/{routeId}"), Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> AssignCargoesToRoute([FromBody] List<int> ids, int routeId)
        {
            await _cargoService.AssignCargoesToRoute(ids, routeId);
            return Ok();
        }
    }
}