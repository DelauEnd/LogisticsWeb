using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Services.Interfaces
{
    public interface IRouteService
    {
        public Task<IEnumerable<RouteDto>> GetAllRoutes();
        public Task<RouteDto> GetRouteById(int routeId);
        public Task AddRoute(RouteForCreationDto route);
        public Task<IEnumerable<CargoDto>> GetCargoesByRouteId(int routeId);
        public Task AssignCargoesToRoute(List<int> ids, int routeId);
        public Task DeleteRouteById(int routeId);
        public Task UpdateRouteById(int orderId, RouteForUpdateDto route);
    }
}
