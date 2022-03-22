using Logistics.Models.Enums;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Logistics.API.Controllers.v1
{
    [Route("api/Transport"), Authorize]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly ITransportService _transportService;

        public TransportController(ITransportService transportService)
        {
            _transportService = transportService;
        }

        /// <summary>
        /// Get list of transport
        /// </summary>
        /// <returns>Returns transport list</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTransport()
        {
            return Ok(await _transportService.GetAllTransport());
        }

        /// <summary>
        /// Get transport by requested id
        /// </summary>
        /// <param name="transportId"></param>
        /// <returns>Returns transport by requested id</returns>
        [HttpGet("{transportId}", Name = "GetTransportById")]
        public async Task<IActionResult> GetTransportById(int transportId)
        {
            return Ok(await _transportService.GetTransportById(transportId));
        }

        /// <summary>
        /// Create new transport
        /// | Required role: Administrator
        /// </summary>
        /// <param name="transport"></param>
        [HttpPost, Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<IActionResult> AddTransport([FromBody] TransportForCreationDto transport)
        {
            await _transportService.AddTransport(transport);
            return Ok();
        }

        /// <summary>
        /// Delete transport by id
        /// | Required role: Administrator
        /// </summary>
        /// <param name="transportId"></param>
        [HttpDelete("{transportId}"), Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<IActionResult> DeleteTransportById(int transportId)
        {
            await _transportService.DeleteTransportById(transportId);
            return Ok();
        }

        /// <summary>
        /// Update transport by id
        /// | Required role: Administrator
        /// </summary>
        /// <param name="transportId"></param>
        /// <param name="patchDoc"></param>
        [HttpPatch("{transportId}"), Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<IActionResult> PartiallyUpdateTransportById(int transportId, [FromBody] JsonPatchDocument<TransportForUpdateDto> patchDoc)
        {
            await _transportService.PatchTransportById(transportId, patchDoc);
            return Ok();
        }
    }
}