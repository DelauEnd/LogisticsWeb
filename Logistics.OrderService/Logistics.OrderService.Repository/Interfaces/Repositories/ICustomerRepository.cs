using Logistics.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetSenderByOrderIdAsync(int id, bool trackChanges);
        Task<Customer> GetDestinationByOrderIdAsync(int id, bool trackChanges);
        Task<IEnumerable<Customer>> GetAllCustomersAsync(bool trackChangess);
        Task<Customer> GetCustomerByIdAsync(int id, bool trackChanges);
        void CreateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
    }
}
