using Logistics.Models.ResponseDTO;
using MediatR;
using System.Collections.Generic;

namespace Logistics.API.Mediatr.Queries.TransportQueries
{
    public class GetAllTransportQuery : IRequest<IEnumerable<TransportDto>>
    {
    }
}
