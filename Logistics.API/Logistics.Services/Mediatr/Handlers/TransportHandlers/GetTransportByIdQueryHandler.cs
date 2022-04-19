using AutoMapper;
using Logistics.Models.ResponseDTO;
using Logistics.Repository.Interfaces;
using Logistics.Services.Mediatr.Queries.TransportQueries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Logistics.Services.Mediatr.Handlers.TransportHandlers
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
