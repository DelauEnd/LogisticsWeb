using Logistics.Entities;
using Logistics.Repository.Interfaces;
using Logistics.Repository.Interfaces.Repositories;
using Logistics.Repository.Repositories;
using System.Threading.Tasks;

namespace Logistics.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly LogisticsDbContext repositoryContext;
        private IRouteRepository routeRepository;
        private ITransportRepository transportRepository;

        public RepositoryManager(LogisticsDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
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
