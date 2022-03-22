using Logistics.Models.Enums;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.API.Controllers.v1
{
    [Route("api/Orders"), Authorize]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get list of orders
        /// </summary>
        /// <returns>Returns orders list</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            return Ok(await _orderService.GetAllOrders());
        }

        /// <summary>
        /// Get order by requested id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Returns order by requested id</returns>
        [HttpGet("{orderId}", Name = "GetOrderById")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            return Ok(await _orderService.GetOrderById(orderId));
        }

        /// <summary>
        /// Create new order
        /// | Required role: Manager
        /// </summary>
        /// <param name="orderToAdd"></param>
        [HttpPost, Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> AddOrder([FromBody] OrderForCreationDto orderToAdd)
        {
            await _orderService.AddOrder(orderToAdd);
            return Ok();
        }

        /// <summary>
        /// Get cargoes by requested order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Returns cargoes by requested order id</returns>
        [HttpGet("{orderId}/Cargoes")]
        public async Task<IActionResult> GetCargoesByOrderId([FromRoute] int orderId)
        {
            return Ok(await _orderService.GetCargoesByOrderId(orderId));
        }

        /// <summary>
        /// Add cargoes to order
        /// | Required role: Manager
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="cargoes"></param>
        [HttpPost("{orderId}/Cargoes"), Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> AddCargoes([FromBody] IEnumerable<CargoForCreationDto> cargoes, [FromRoute] int orderId)
        {
            await _orderService.AddCargoesToOrder(cargoes, orderId);
            return Ok();
        }

        /// <summary>
        /// Delete order by id
        /// | Required role: Manager
        /// </summary>
        /// <param name="orderId"></param>
        [HttpDelete("{orderId}"), Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> DeleteOrderById(int orderId)
        {
            await _orderService.DeleteOrderById(orderId);
            return Ok();
        }

        /// <summary>
        /// Update order by id
        /// | Required role: Manager
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="patchDoc"></param>
        [HttpPatch("{orderId}"), Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> PartiallyUpdateOrderById(int orderId, [FromBody] JsonPatchDocument<OrderForUpdateDto> patchDoc)
        {
            await _orderService.PatchOrderById(orderId, patchDoc);
            return Ok();
        }
    }
}