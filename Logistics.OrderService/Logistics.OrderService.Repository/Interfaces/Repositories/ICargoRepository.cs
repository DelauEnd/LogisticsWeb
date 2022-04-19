using Logistics.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.OrderService.Repository.Interfaces.Repositories
{
    public interface ICargoRepository
    {
        Task<IEnumerable<Cargo>> GetCargoesByOrderIdAsync(int id, bool trackChanges);
        Task<IEnumerable<Cargo>> GetCargoesByRouteIdAsync(int id, bool trackChanges);
        void CreateCargoForOrder(Cargo cargo, int orderId);
        Task AssignCargoToRoute(int cargoIds, int routeId);
        Task<Cargo> GetCargoByIdAsync(int cargoId, bool trackChanges);
        void DeleteCargo(Cargo cargo);
        Task<IEnumerable<Cargo>> GetAllCargoesAsync(bool trackChanges);
        Task<IEnumerable<Cargo>> GetUnassignedCargoesAsync(bool trackChanges);
    }
}
