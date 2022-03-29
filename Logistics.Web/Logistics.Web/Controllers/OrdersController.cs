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
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CargoTransportation.Controllers
{
    [Authorize]
    [Route("Orders")]
    public class OrdersController : Controller
    {
        private static OrderForUpdateDto OrderToUpdate { get; set; }

        private readonly IOrderRequestHandler _orderHandler;
        private readonly ICustomerRequestHandler _customerHandler;
        public OrdersController(IOrderRequestHandler orderHandler, ICustomerRequestHandler customerHandler)
        {
            _customerHandler = customerHandler;
            _orderHandler = orderHandler;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var response = await _orderHandler.GetAllOrders();
            IEnumerable<OrderDto> orders;

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            orders = JsonConvert.DeserializeObject<IEnumerable<OrderDto>>(await response.Content.ReadAsStringAsync());
            return View(orders);
        }

        [HttpGet]
        [Route("{id}/Cargoes")]
        public async Task<ActionResult> Cargoes(int id)
        {
            var response = await _orderHandler.GetCargoesForOrder(id);
            IEnumerable<CargoDto> cargoes;

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            ViewBag.OrderToEdit = id;

            cargoes = JsonConvert.DeserializeObject<IEnumerable<CargoDto>>(await response.Content.ReadAsStringAsync());

            return View(cargoes);
        }

        [HttpGet]
        [Route("Create")]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> Create()
        {
            var customersResponse = await _customerHandler.GetAllCustomers();

            if (!customersResponse.IsSuccessStatusCode)
                return new StatusCodeResult((int)customersResponse.StatusCode);

            var customers = JsonConvert.DeserializeObject<IEnumerable<CustomerDto>>(await customersResponse.Content.ReadAsStringAsync());

            ViewBag.customers = customers;

            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> Create(OrderForCreationDto order)
        {
            HttpContent content = HttpContentBuilder.BuildContent(order);
            var response = await _orderHandler.CreateOrder(content);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{id}/Edit")]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> Edit(int id)
        {
            var response = await _orderHandler.GetOrderById(id);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            var order = JsonConvert.DeserializeObject<OrderDto>(await response.Content.ReadAsStringAsync());

            var customersResponse = await _customerHandler.GetAllCustomers();
            var customers = JsonConvert.DeserializeObject<IEnumerable<CustomerDto>>(await customersResponse.Content.ReadAsStringAsync());

            ViewBag.customers = customers;

            OrderToUpdate = new OrderForUpdateDto
            {
                Status = order.Status,
                DestinationId = customers.Where(customer => customer.Address == order.Destination).FirstOrDefault().Id,
                SenderId = customers.Where(customer => customer.Address == order.Sender).FirstOrDefault().Id
            };

            return View(OrderToUpdate);
        }

        [HttpPost]
        [Route("{id}/Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> Edit(int id, OrderForUpdateDto order)
        {
            var jsonPatch = JsonPatcher.CreatePatch(OrderToUpdate, order);

            HttpContent content = HttpContentBuilder.BuildContent(jsonPatch);
            var response = await _orderHandler.PatchOrderById(id, content);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{id}/Delete")]
        [Authorize(Roles = nameof(UserRole.Manager))]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _orderHandler.DeleteOrderById(id);

            if (!response.IsSuccessStatusCode)
                return new StatusCodeResult((int)response.StatusCode);

            return RedirectToAction("Index");
        }
    }
}