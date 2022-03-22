using Logistics.Models.Enums;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.API.Controllers.v1
{
    [Route("api/Routes"), Authorize]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly IRouteService _routeService;

        public RoutesController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        /// <summary>
        /// Get list of routes
        /// </summary>
        /// <returns>Returns routes list</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRoutes()
        {
            return Ok(await _routeService.GetAllRoutes());
        }

        /// <summary>
        /// Get route by requested id
        /// </summary>
        /// <param name="routeId"></param>
        /// <returns>Returns route by requested id</returns>
        [HttpGet("{routeId}", Name = "GetRouteById")]
        public async Task<IActionResult> GetRouteById(int routeId)
        {
            return Ok(await _routeService.GetRouteById(routeId));
        }

        /// <summary>
        /// Create new route
        /// | Required role: Manager
        /// </summary>
        /// <param name="route"></param>
        [HttpPost, Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> AddRoute([FromBody] RouteForCreationDto route)
        {
            await _routeService.AddRoute(route);
            return Ok();
        }

        /// <summary>
        /// Delete route by id
        /// | Required role: Manager
        /// </summary>
        /// <param name="routeId"></param>
        [HttpDelete("{routeId}"), Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> DeleteRouteById(int routeId)
        {
            await _routeService.DeleteRouteById(routeId);
            return Ok();
        }

        /// <summary>
        /// Update route by id
        /// | Required role: Manager
        /// </summary>
        /// <param name="routeId"></param>
        /// <param name="route"></param>
        [HttpPut("{routeId}"), Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> UpdateRouteById(int routeId, RouteForUpdateDto route)
        {
            await _routeService.UpdateRouteById(routeId, route);
            return Ok();
        }

        /// <summary>
        /// Get cargoes by requested route id
        /// </summary>
        /// <param name="routeId"></param>
        /// <returns>Returns cargoes by requested order id</returns>
        [HttpGet("{routeId}/Cargoes")]
        public async Task<IActionResult> GetCargoesByRouteId(int routeId)
        {
            return Ok(await _routeService.GetCargoesByRouteId(routeId));
        }

        /// <summary>
        /// Mark cargo by requested id to route by requested id
        /// | Required role: Manager
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="routeId"></param>
        [HttpPost("{routeId}/Cargoes"), Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> AssignCargoesToRoute([FromBody] List<int> ids, int routeId)
        {
            await _routeService.AssignCargoesToRoute(ids, routeId);
            return Ok();
        }
    }
}