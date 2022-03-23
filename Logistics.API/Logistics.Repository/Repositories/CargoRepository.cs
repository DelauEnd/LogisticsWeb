using Logistics.Entities;
using Logistics.Entities.Models;
using Logistics.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.Repository.Repositories
{
    public class CargoRepository : RepositoryBase<Cargo>, ICargoRepository
    {
        public CargoRepository(LogisticsDbContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateCargoForOrder(Cargo cargo, int orderId)
        {
            cargo.OrderId = orderId;
            Create(cargo);
        }

        public void DeleteCargo(Cargo cargo)
            => Delete(cargo);


        public async Task<IEnumerable<Cargo>> GetAllCargoesAsync(bool trackChanges)
        {
            var cargoes = await FindAll(trackChanges)
                .Include(cargo => cargo.Category)
                .ToListAsync();

            return cargoes;
        }

        public async Task<Cargo> GetCargoByIdAsync(int id, bool trackChanges)
            => await FindByCondition(cargo => cargo.Id == id, trackChanges)
                .Include(cargo => cargo.Category)
                .SingleOrDefaultAsync();


        public async Task<IEnumerable<Cargo>> GetCargoesByOrderIdAsync(int id, bool trackChanges)
        {
            var cargoes = await FindByCondition(cargo =>
            cargo.OrderId == id, trackChanges)
                .Include(cargo => cargo.Category)
                .ToListAsync();

            return cargoes;
        }

        public async Task<IEnumerable<Cargo>> GetCargoesByRouteIdAsync(int id, bool trackChanges)
        {
            var cargoes = await FindByCondition(cargo =>
            cargo.RouteId == id, trackChanges)
                .Include(cargo => cargo.Category)
                .ToListAsync();

            return cargoes;
        }

        public async Task AssignCargoToRoute(int cargoId, int routeId)
        {
            var cargo = await FindByCondition(cargo =>
            cargo.Id == cargoId, true)
                .FirstOrDefaultAsync();

            cargo.RouteId = routeId;        
        }

        public async Task<IEnumerable<Cargo>> GetUnassignedCargoesAsync(bool trackChanges)
        {
            var cargoes = await FindAll(trackChanges)
                .Include(cargo => cargo.Category)
                .Where(cargo => cargo.RouteId == null)
                .ToListAsync();

            return cargoes;
        }
    }
}
