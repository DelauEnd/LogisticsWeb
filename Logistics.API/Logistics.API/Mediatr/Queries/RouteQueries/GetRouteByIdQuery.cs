using Logistics.Models.ResponseDTO;
using MediatR;

namespace Logistics.API.Mediatr.Queries.RouteQueries
{
    public class GetRouteByIdQuery : IRequest<RouteDto>
    {
        public int RouteId { get; }
        public GetRouteByIdQuery(int routeId)
        {
            RouteId = routeId;
        }
    }
}
