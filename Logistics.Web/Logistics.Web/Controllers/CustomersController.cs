using CargoTransportation.Utils;
using Logistics.Models.Enums;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Logistics.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RequestHandler.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CargoTransportation.Controllers
{
    [Authorize]
    [Route("Customers")]
    public class CustomersController : Controller
    {
        private static CustomerForUpdateDto CustomerToUpdate { get; set; }

        private readonly ICustomerRequestHandler _customerHandler;
        public CustomersController(ICustomerRequestHandler customerHandler)
        {
            _customerHandler = customerHandler;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            ViewBag.SelectedTab = "Customers";

            var response = await _customerHandler.GetAllCustomers();

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            var customers = JsonConvert.DeserializeObject<IEnumerable<CustomerDto>>(await response.Content.ReadAsStringAsync());
            return View(customers);
        }

        [HttpGet]
        [Route("Create")]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public ActionResult Create()
        {
            ViewBag.SelectedTab = "Customers";
            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> Create(CustomerForCreationDto customer)
        {
            HttpContent content = HttpContentBuilder.BuildContent(customer);
            var response = await _customerHandler.CreateCustomer(content);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{id}/Delete")]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _customerHandler.DeleteCustomerById(id);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{id}/Edit")]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.SelectedTab = "Customers";

            var response = await _customerHandler.GetCustomerById(id);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            var customer = JsonConvert.DeserializeObject<CustomerDto>(await response.Content.ReadAsStringAsync());

            CustomerToUpdate = new CustomerForUpdateDto
            {
                Address = customer.Address,
                ContactPerson = customer.ContactPerson
            };

            return View(CustomerToUpdate);
        }

        [HttpPost]
        [Route("{id}/Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> Edit(int id, CustomerForUpdateDto customer)
        {
            var jsonPatch = JsonPatcher.CreatePatch(CustomerToUpdate, customer);

            HttpContent content = HttpContentBuilder.BuildContent(jsonPatch);
            var response = await _customerHandler.PatchCustomerById(id, content);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction(nameof(Index));
        }
    }
}