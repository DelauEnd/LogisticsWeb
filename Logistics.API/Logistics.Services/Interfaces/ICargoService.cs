using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Services.Interfaces
{
    public interface ICargoService
    {
        public Task<IEnumerable<CargoDto>> GetAllCargoes();
        public Task<IEnumerable<CargoDto>> GetUnassignedCargoes();
        public Task<CargoDto> GetCargoById(int cargoId);
        public Task DeleteCargoById(int cargoId);
        public Task PatchCargoById(int cargoId, JsonPatchDocument<CargoForUpdateDto> patchDoc);
    }
}
