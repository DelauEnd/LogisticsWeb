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
    [Route("api/Customers"), Authorize]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Get list of customers
        /// </summary>
        /// <returns>Returns customers list</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            return Ok(await _customerService.GetAllCustomers());
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns>Returns requested customer</returns>
        [HttpGet("{customerId}", Name = "GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            return Ok(await _customerService.GetCustomerById(customerId));
        }

        /// <summary>
        /// Create new customer by id
        /// | Required role: Manager
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Returns added customer</returns>
        [HttpPost, Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerForCreationDto customer)
        {
            return Ok(await _customerService.AddCustomer(customer));
        }

        /// <summary>
        /// Delete customer by id
        /// | Required role: Manager
        /// </summary>
        /// <param name="customerId"></param>
        [HttpDelete("{customerId}"), Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> DeleteCustomerById(int customerId)
        {
            await _customerService.DeleteCustomerById(customerId);
            return Ok();
        }

        /// <summary>
        /// Update customer by id
        /// | Required role: Manager
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="patchDoc"></param>
        /// <returns>Returns updated customer</returns>
        [HttpPatch("{customerId}"), Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<IActionResult> PartiallyUpdateCustomerById(int customerId, [FromBody] JsonPatchDocument<CustomerForUpdateDto> patchDoc)
        {
            return Ok(await _customerService.PatchCustomerById(customerId, patchDoc));
        }
    }
}