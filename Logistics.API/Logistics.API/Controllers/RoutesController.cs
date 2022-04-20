using Logistics.API.Mediatr.Commands.RouteCommands;
using Logistics.API.Mediatr.Queries.RouteQueries;
using Logistics.Entities.Enums;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Logistics.API.Controllers
{
    [Route("api/Routes"), Authorize]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoutesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get list of routes
        /// </summary>
        /// <returns>Returns routes list</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRoutes()
        {
            var query = new GetAllRoutesQuery();

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Get route by requested id
        /// </summary>
        /// <param name="routeId"></param>
        /// <returns>Returns route by requested id</returns>
        [HttpGet("{routeId}", Name = "GetRouteById")]
        public async Task<IActionResult> GetRouteById(int routeId)
        {
            var query = new GetRouteByIdQuery(routeId);

            return Ok(await _mediator.Send(query));
        }

        /// <summary>
        /// Create new route
        /// | Required role: Manager
        /// </summary>
        /// <param name="route"></param>
        /// <returns>Returns added route</returns>
        [HttpPost, Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> AddRoute([FromBody] RouteForCreationDto route)
        {
            var command = new AddRouteCommand(route);

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// Delete route by id
        /// | Required role: Manager
        /// </summary>
        /// <param name="routeId"></param>
        [HttpDelete("{routeId}"), Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> DeleteRouteById(int routeId)
        {
            var command = new DeleteRouteByIdCommand(routeId);
            await _mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Update route by id
        /// | Required role: Manager
        /// </summary>
        /// <param name="routeId"></param>
        /// <param name="route"></param>
        /// <returns>Returns updated route</returns>
        [HttpPut("{routeId}"), Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> UpdateRouteById(int routeId, RouteForUpdateDto route)
        {
            var command = new UpdateRouteByIdCommand(route, routeId);

            return Ok(await _mediator.Send(command));
        }
    }
}