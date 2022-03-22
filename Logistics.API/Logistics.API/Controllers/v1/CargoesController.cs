using Logistics.Models.Enums;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Logistics.API.Controllers.v1
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
        [HttpPatch("{cargoId}"), Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> PartiallyUpdateCargoById(int cargoId, [FromBody] JsonPatchDocument<CargoForUpdateDto> patchDoc)
        {
            await _cargoService.PatchCargoById(cargoId, patchDoc);
            return Ok();
        }
    }
}