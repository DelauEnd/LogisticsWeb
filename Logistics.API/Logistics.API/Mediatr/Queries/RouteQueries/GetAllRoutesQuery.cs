using Logistics.Models.ResponseDTO;
using MediatR;
using System.Collections.Generic;

namespace Logistics.API.Mediatr.Queries.RouteQueries
{
    public class GetAllRoutesQuery : IRequest<IEnumerable<RouteDto>>
    {
    }
}
