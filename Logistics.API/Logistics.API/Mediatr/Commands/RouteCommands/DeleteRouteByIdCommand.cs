using MediatR;

namespace Logistics.API.Mediatr.Commands.RouteCommands
{
    public class DeleteRouteByIdCommand : IRequest
    {
        public int RouteId { get; }
        public DeleteRouteByIdCommand(int routeId)
        {
            RouteId = routeId;
        }
    }
}
