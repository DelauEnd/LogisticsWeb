using Logistics.Entities;
using Logistics.Entities.Models;
using Logistics.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Repository.Repositories
{
    public class RouteRepository : RepositoryBase<Route>, IRouteRepository
    {
        public RouteRepository(LogisticsDbContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<Route> GetRouteByIdAsync(int id, bool trackChanges)
            => await FindByCondition(route => route.Id == id, trackChanges)
                .Include(route => route.Cargoes).ThenInclude(cargo => cargo.Category)
                .Include(route => route.Transport)
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Route>> GetAllRoutesAsync(bool trackChanges)
            => await FindAll(trackChanges)
                .Include(route => route.Transport)
                .ToListAsync();

        public void CreateRoute(Route route)
            => Create(route);

        public void DeleteRoute(Route route)
            => Delete(route);
    }
}
