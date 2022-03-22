using CargoTransportation.ActionFilters;
using CargoTransportation.Utils;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CargoTransportation.Controllers
{
    [Route("Orders")]
    [ServiceFilter(typeof(AuthenticatedAttribute))]
    public class OrdersController : ExtendedControllerBase
    {
        private static OrderForUpdateDto OrderToUpdate { get; set; }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var response = await request.OrderRequestHandler.GetAllOrders();
            IEnumerable<OrderDto> orders;

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            orders = JsonConvert.DeserializeObject<IEnumerable<OrderDto>>(await response.Content.ReadAsStringAsync());
            return View(orders);
        }

        [HttpGet]
        [Route("{id}/Cargoes")]
        public async Task<ActionResult> Cargoes(int id)
        {
            var response = await request.OrderRequestHandler.GetCargoesForOrder(id);
            IEnumerable<CargoDto> cargoes;

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            ViewBag.OrderToEdit = id;

            cargoes = JsonConvert.DeserializeObject<IEnumerable<CargoDto>>(await response.Content.ReadAsStringAsync());

            return View(cargoes);
        }

        [HttpGet]
        [Route("Create")]
        [ServiceFilter(typeof(HasManagerRole))]
        public async Task<ActionResult> Create()
        {
            var customersResponse = await request.CustomerRequestHandler.GetAllCustomers();

            if (!customersResponse.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(customersResponse);

            var customers = JsonConvert.DeserializeObject<IEnumerable<CustomerDto>>(await customersResponse.Content.ReadAsStringAsync());

            ViewBag.customers = customers;

            return View();
        }

        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        [ServiceFilter(typeof(HasManagerRole))]
        public async Task<ActionResult> Create(OrderForCreationDto order)
        {
            HttpContent content = BuildHttpContent(order);
            var response = await request.OrderRequestHandler.CreateOrder(content);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{id}/Edit")]
        [ServiceFilter(typeof(HasManagerRole))]
        public async Task<ActionResult> Edit(int id)
        {
            var response = await request.OrderRequestHandler.GetOrderById(id);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            var order = JsonConvert.DeserializeObject<OrderDto>(await response.Content.ReadAsStringAsync());

            var customersResponse = await request.CustomerRequestHandler.GetAllCustomers();
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
        [ServiceFilter(typeof(HasManagerRole))]
        public async Task<ActionResult> Edit(int id, OrderForUpdateDto order)
        {
            var jsonPatch = JsonPatcher.CreatePatch(OrderToUpdate, order);

            HttpContent content = BuildHttpContent(jsonPatch);
            var response = await request.OrderRequestHandler.PatchOrderById(id, content);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("{id}/Delete")]
        [ServiceFilter(typeof(HasManagerRole))]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await request.OrderRequestHandler.DeleteOrderById(id);

            if (!response.IsSuccessStatusCode)
                return UnsuccesfullStatusCode(response);

            return RedirectToAction("Index");
        }
    }
}