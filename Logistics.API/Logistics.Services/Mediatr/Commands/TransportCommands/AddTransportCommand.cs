using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.ResponseDTO;
using MediatR;

namespace Logistics.Services.Mediatr.Commands.TransportCommands
{
    public class AddTransportCommand : IRequest<TransportDto>
    {
        public TransportForCreationDto TransportForCreation { get; }
        public AddTransportCommand(TransportForCreationDto transportForCreation)
        {
            TransportForCreation = transportForCreation;
        }
    }
}
