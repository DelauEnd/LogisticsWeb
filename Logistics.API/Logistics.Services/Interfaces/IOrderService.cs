using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDto>> GetAllOrders();
        public Task<OrderDto> GetOrderById(int orderId);
        public Task AddOrder(OrderForCreationDto order);
        public Task<IEnumerable<CargoDto>> GetCargoesByOrderId(int orderId);
        public Task AddCargoesToOrder(IEnumerable<CargoForCreationDto> cargoes, int orderId);
        public Task DeleteOrderById(int orderId);
        public Task PatchOrderById(int orderId, JsonPatchDocument<OrderForUpdateDto> patchDoc);
    }
}
