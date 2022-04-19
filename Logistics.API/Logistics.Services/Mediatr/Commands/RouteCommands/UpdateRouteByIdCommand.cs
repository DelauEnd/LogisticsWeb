using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using MediatR;

namespace Logistics.Services.Mediatr.Commands.RouteCommands
{
    public class UpdateRouteByIdCommand : IRequest<RouteDto>
    {
        public RouteForUpdateDto RouteForUpdate { get; }
        public int RouteId { get; }

        public UpdateRouteByIdCommand(RouteForUpdateDto routeForUpdate, int routeId)
        {
            RouteForUpdate = routeForUpdate;
            RouteId = routeId;
        }
    }
}
