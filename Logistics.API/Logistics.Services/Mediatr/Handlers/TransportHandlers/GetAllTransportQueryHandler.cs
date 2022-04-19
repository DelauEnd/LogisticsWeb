using AutoMapper;
using Logistics.Models.ResponseDTO;
using Logistics.Repository.Interfaces;
using Logistics.Services.Mediatr.Queries.TransportQueries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Logistics.Services.Mediatr.Handlers.TransportHandlers
{
    public class GetAllTransportQueryHandler : IRequestHandler<GetAllTransportQuery, IEnumerable<TransportDto>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public GetAllTransportQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<TransportDto>> Handle(GetAllTransportQuery request, CancellationToken cancellationToken)
        {
            var transpor = await _repository.Transport.GetAllTransportAsync(false);
            var transportDto = _mapper.Map<IEnumerable<TransportDto>>(transpor);
            return transportDto;
        }
    }
}
