using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Services.Interfaces
{
    public interface ITransportService
    {
        public Task<IEnumerable<TransportDto>> GetAllTransport();
        public Task<TransportDto> GetTransportById(int transportId);
        public Task AddTransport(TransportForCreationDto transport);
        public Task DeleteTransportById(int transportId);
        public Task PatchTransportById(int transportId, JsonPatchDocument<TransportForUpdateDto> patchDoc);
    }
}
