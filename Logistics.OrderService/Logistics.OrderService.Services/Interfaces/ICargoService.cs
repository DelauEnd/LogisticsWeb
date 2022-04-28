using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.OrderService.Services.Interfaces
{
    public interface ICargoService
    {
        public Task<IEnumerable<CargoDto>> GetAllCargoes();
        public Task<IEnumerable<CargoDto>> GetUnassignedCargoes();
        public Task<CargoDto> GetCargoById(int cargoId);
        public Task DeleteCargoById(int cargoId);
        public Task<CargoDto> PatchCargoById(int cargoId, JsonPatchDocument<CargoForUpdateDto> patchDoc);
        Task AssignCargoesToRoute(List<int> ids, int routeId);
        Task<IEnumerable<CargoDto>> GetCargoesByRouteId(int routeId);
    }
}
