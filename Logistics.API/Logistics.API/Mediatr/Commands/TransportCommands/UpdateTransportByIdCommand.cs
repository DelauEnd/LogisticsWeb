using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Logistics.API.Mediatr.Commands.TransportCommands
{
    public class UpdateTransportByIdCommand : IRequest<TransportDto>
    {
        public JsonPatchDocument<TransportForUpdateDto> TransportPatchDoc { get; }
        public int TransportId { get; }

        public UpdateTransportByIdCommand(int transportId, JsonPatchDocument<TransportForUpdateDto> transportPatchDoc)
        {
            TransportId = transportId;
            TransportPatchDoc = transportPatchDoc;
        }
    }
}
