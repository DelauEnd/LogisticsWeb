using Logistics.Models.ResponseDTO;
using MediatR;

namespace Logistics.Services.Mediatr.Queries.TransportQueries
{
    public class GetTransportByIdQuery : IRequest<TransportDto>
    {
        public int TransportId { get; }

        public GetTransportByIdQuery(int transportId)
        {
            TransportId = transportId;
        }
    }
}
