using AutoMapper;
using Logistics.Entities.Models;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Logistics.Repository.Interfaces;
using Logistics.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Services.Services
{
    public class OrderService : IOrderService
    {
        readonly IRepositoryManager _repository;
        readonly IMapper _mapper;
        public OrderService(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AddCargoesToOrder(IEnumerable<CargoForCreationDto> cargoes, int orderId)
        {
            var addableCargoes = _mapper.Map<IEnumerable<Cargo>>(cargoes);

            foreach (var cargo in addableCargoes)
                _repository.Cargoes.CreateCargoForOrder(cargo, orderId);
            await _repository.SaveAsync();
        }

        public async Task<OrderDto> AddOrder(OrderForCreationDto orderToAdd)
        {
            var order = _mapper.Map<Order>(orderToAdd);
            _repository.Orders.CreateOrder(order);
            await _repository.SaveAsync();

            var orderWithIncludes = await _repository.Orders.GetOrderByIdAsync(order.Id, false);
            return _mapper.Map<OrderDto>(orderWithIncludes);
        }

        public async Task DeleteOrderById(int orderId)
        {
            var order = await _repository.Orders.GetOrderByIdAsync(orderId, false);
            _repository.Orders.DeleteOrder(order);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            var orders = await _repository.Orders.GetAllOrdersAsync(false);
            var ordersDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return ordersDto;
        }

        public async Task<IEnumerable<CargoDto>> GetCargoesByOrderId(int orderId)
        {
            var order = await _repository.Orders.GetOrderByIdAsync(orderId, false);
            var cargoes = await _repository.Cargoes.GetCargoesByOrderIdAsync(order.Id, false);
            var cargoesDto = _mapper.Map<IEnumerable<CargoDto>>(cargoes);

            return cargoesDto;
        }

        public async Task<OrderDto> GetOrderById(int orderId)
        {
            var order = await _repository.Orders.GetOrderByIdAsync(orderId, false);
            var orderDto = _mapper.Map<OrderDto>(order);
            return orderDto;
        }

        public async Task<OrderDto> PatchOrderById(int orderId, JsonPatchDocument<OrderForUpdateDto> patchDoc)
        {
            var order = await _repository.Orders.GetOrderByIdAsync(orderId, false);

            var orderToPatch = _mapper.Map<OrderForUpdateDto>(order);
            patchDoc.ApplyTo(orderToPatch);

            _mapper.Map(orderToPatch, order);

            await _repository.SaveAsync();

            var orderWithIncludes = await _repository.Orders.GetOrderByIdAsync(order.Id, false);
            return _mapper.Map<OrderDto>(orderWithIncludes); 
        }
    }
}
