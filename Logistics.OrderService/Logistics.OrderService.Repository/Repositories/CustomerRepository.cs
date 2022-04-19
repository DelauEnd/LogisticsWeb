using Logistics.Entities;
using Logistics.Entities.Models;
using Logistics.OrderService.Repository.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.OrderService.Repository.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(LogisticsDbContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateCustomer(Customer customer)
            => Create(customer);

        public void DeleteCustomer(Customer customer)
            => Delete(customer);

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(bool trackChangess)
            => await FindAll(trackChangess)
            .ToListAsync();

        public async Task<Customer> GetCustomerByIdAsync(int id, bool trackChanges)
            => await FindByCondition(destination =>
                destination.Id == id, trackChanges)
                .SingleOrDefaultAsync();

        public async Task<Customer> GetDestinationByOrderIdAsync(int id, bool trackChanges)
            => await FindByCondition(destination =>
                destination.OrderDestination.Any(order => order.Id == id), trackChanges)
                .SingleOrDefaultAsync();

        public async Task<Customer> GetSenderByOrderIdAsync(int id, bool trackChanges)
            => await FindByCondition(sender =>
                sender.OrderSender.Any(order => order.Id == id), trackChanges)
                .SingleOrDefaultAsync();
    }
}
