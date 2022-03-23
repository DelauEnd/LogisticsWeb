using Logistics.Entities;
using Logistics.Repository.Interfaces;
using Logistics.Repository.Repositories;
using System.Threading.Tasks;

namespace Logistics.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly LogisticsDbContext repositoryContext;
        private ICargoCategoryRepository cargoCategoryRepository;
        private ICargoRepository cargoRepository;
        private ICustomerRepository customerRepository;
        private IOrderRepository orderRepository;
        private IRouteRepository routeRepository;
        private ITransportRepository transportRepository;

        public RepositoryManager(LogisticsDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
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
        public ICargoRepository Cargoes
        {
            get
            {
                if (cargoRepository == null)
                    cargoRepository = new CargoRepository(repositoryContext);
                return cargoRepository;
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
        public IOrderRepository Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(repositoryContext);
                return orderRepository;
            }
        }
        public IRouteRepository Routes
        {
            get
            {
                if (routeRepository == null)
                    routeRepository = new RouteRepository(repositoryContext);
                return routeRepository;
            }
        }

        public ITransportRepository Transport
        {
            get
            {
                if (transportRepository == null)
                    transportRepository = new TransportRepository(repositoryContext);
                return transportRepository;
            }
        }

        public async Task SaveAsync()
            => await repositoryContext.SaveChangesAsync();

    }
}
