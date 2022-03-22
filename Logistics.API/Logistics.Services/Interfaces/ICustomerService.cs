using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Services.Interfaces
{
    public interface ICustomerService
    {
        public Task<IEnumerable<CustomerDto>> GetAllCustomers();
        public Task AddCustomer(CustomerForCreationDto customerToAdd);
        public Task<CustomerDto> GetCustomerById(int customerId);
        public Task DeleteCustomerById(int customerId);
        public Task PatchCustomerById(int customerId, JsonPatchDocument<CustomerForUpdateDto> patchDoc);
    }
}
