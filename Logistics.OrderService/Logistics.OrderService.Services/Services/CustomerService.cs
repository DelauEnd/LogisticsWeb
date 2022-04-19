using AutoMapper;
using Logistics.Entities.Models;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Logistics.OrderService.Repository.Interfaces;
using Logistics.OrderService.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.OrderService.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public CustomerService(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CustomerDto> AddCustomer(CustomerForCreationDto customerToAdd)
        {
            var customer = _mapper.Map<Customer>(customerToAdd);
            _repository.Customers.CreateCustomer(customer);
            await _repository.SaveAsync();

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task DeleteCustomerById(int customerId)
        {
            var customer = await _repository.Customers.GetCustomerByIdAsync(customerId, false);
            _repository.Customers.DeleteCustomer(customer);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            var customers = await _repository.Customers.GetAllCustomersAsync(false);
            var customersDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            return customersDto;
        }

        public async Task<CustomerDto> GetCustomerById(int customerId)
        {
            var customer = await _repository.Customers.GetCustomerByIdAsync(customerId, false);
            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;
        }

        public async Task<CustomerDto> PatchCustomerById(int customerId, JsonPatchDocument<CustomerForUpdateDto> patchDoc)
        {
            var customer = await _repository.Customers.GetCustomerByIdAsync(customerId, false);

            var customerToPatch = _mapper.Map<CustomerForUpdateDto>(customer);
            patchDoc.ApplyTo(customerToPatch);

            _mapper.Map(customerToPatch, customer);

            await _repository.SaveAsync();

            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
