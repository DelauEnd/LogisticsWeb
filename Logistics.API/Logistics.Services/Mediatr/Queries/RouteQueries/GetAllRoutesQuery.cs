using Logistics.Models.ResponseDTO;
using MediatR;
using System.Collections.Generic;

namespace Logistics.Services.Mediatr.Queries.RouteQueries
{
    public class GetAllRoutesQuery : IRequest<IEnumerable<RouteDto>>
    {
    }
}
