﻿using Logistics.Models.Enums;
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
        /// <returns>Returns added route</returns>
        [HttpPost, Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> AddRoute([FromBody] RouteForCreationDto route)
        {
            return Ok(await _routeService.AddRoute(route));
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
        /// <returns>Returns updated route</returns>
        [HttpPut("{routeId}"), Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> UpdateRouteById(int routeId, RouteForUpdateDto route)
        {
            return Ok(await _routeService.UpdateRouteById(routeId, route));
        }
    }
}