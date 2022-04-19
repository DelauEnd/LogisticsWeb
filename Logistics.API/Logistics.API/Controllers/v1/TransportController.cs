using Logistics.Models.Enums;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Services.Mediatr.Commands;
using Logistics.Services.Mediatr.Queries;
using MediatR;
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
        private readonly IMediator _mediator;

        public TransportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get list of transport
        /// </summary>
        /// <returns>Returns transport list</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllTransport()
        {
            var query = new GetAllTransportQuery();

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Get transport by requested id
        /// </summary>
        /// <param name="transportId"></param>
        /// <returns>Returns transport by requested id</returns>
        [HttpGet("{transportId}", Name = "GetTransportById")]
        public async Task<IActionResult> GetTransportById(int transportId)
        {
            var query = new GetTransportByIdQuery(transportId);

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Create new transport
        /// | Required role: Administrator
        /// </summary>
        /// <param name="transport"></param>
        /// <returns>Returns added transport</returns>
        [HttpPost, Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<IActionResult> AddTransport([FromBody] TransportForCreationDto transport)
        {
            var command = new AddTransportCommand(transport);

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete transport by id
        /// | Required role: Administrator
        /// </summary>
        /// <param name="transportId"></param>
        [HttpDelete("{transportId}"), Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<IActionResult> DeleteTransportById(int transportId)
        {
            var command = new DeleteTransportByIdCommand(transportId);
            await _mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Update transport by id
        /// | Required role: Administrator
        /// </summary>
        /// <param name="transportId"></param>
        /// <param name="patchDoc"></param>
        /// <returns>Returns updated transport</returns>
        [HttpPatch("{transportId}"), Authorize(Roles = nameof(UserRole.Administrator))]
        public async Task<IActionResult> PartiallyUpdateTransportById(int transportId, [FromBody] JsonPatchDocument<TransportForUpdateDto> patchDoc)
        {
            var command = new UpdateTransportByIdCommand(transportId, patchDoc);

            return Ok(await _mediator.Send(command));
        }
    }
}