using Logistics.Entities;
using Logistics.Repository.Interfaces;
using Logistics.Repository.Repositories;
using System.Threading.Tasks;

namespace Logistics.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly LogisticsDbContext repositoryContext;
        private ICargoRepository cargoRepository;
        private IOrderRepository orderRepository;

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

        public async Task SaveAsync()
            => await repositoryContext.SaveChangesAsync();

    }
}
