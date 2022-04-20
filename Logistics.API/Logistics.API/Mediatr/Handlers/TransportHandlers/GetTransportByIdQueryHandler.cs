using AutoMapper;
using Logistics.API.Mediatr.Queries.TransportQueries;
using Logistics.Models.ResponseDTO;
using Logistics.Repository.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Logistics.API.Mediatr.Handlers.TransportHandlers
{
    public class GetTransportByIdQueryHandler : IRequestHandler<GetTransportByIdQuery, TransportDto>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public GetTransportByIdQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<TransportDto> Handle(GetTransportByIdQuery request, CancellationToken cancellationToken)
        {
            var transport = await _repository.Transport.GetTransportByIdAsync(request.TransportId, false);
            var transportDto = _mapper.Map<TransportDto>(transport);
            return transportDto;
        }
    }
}
