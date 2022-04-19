using Logistics.Models.ResponseDTO;
using MediatR;
using System.Collections.Generic;

namespace Logistics.Services.Mediatr.Queries.TransportQueries
{
    public class GetAllTransportQuery : IRequest<IEnumerable<TransportDto>>
    {
    }
}
