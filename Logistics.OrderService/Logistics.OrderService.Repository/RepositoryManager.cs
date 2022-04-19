using Logistics.Entities;
using Logistics.OrderService.Repository.Interfaces;
using Logistics.OrderService.Repository.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Logistics.OrderService.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly LogisticsDbContext repositoryContext;
        private ICargoRepository cargoRepository;
        private IOrderRepository orderRepository;
        private ICargoCategoryRepository cargoCategoryRepository;
        private ICustomerRepository customerRepository;

        public RepositoryManager(LogisticsDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }

        public ICargoRepository Cargoes
        {
            get
            {
                if (cargoRepository == null)
                    cargoRepository = new CargoRepository(repositoryContext);
                return cargoRepository;
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(repositoryContext);
                return orderRepository;
            }
        }

        public ICargoCategoryRepository CargoCategories
        {
            get
            {
                if (cargoCategoryRepository == null)
                    cargoCategoryRepository = new CargoCategoryRepository(repositoryContext);
                return cargoCategoryRepository;
            }
        }

        public ICustomerRepository Customers
        {
            get
            {
                if (customerRepository == null)
                    customerRepository = new CustomerRepository(repositoryContext);
                return customerRepository;
            }
        }

        public async Task SaveAsync()
            => await repositoryContext.SaveChangesAsync();

    }
}
