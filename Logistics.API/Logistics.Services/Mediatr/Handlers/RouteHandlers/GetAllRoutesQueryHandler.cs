using AutoMapper;
using Logistics.Models.ResponseDTO;
using Logistics.Repository.Interfaces;
using Logistics.Services.Mediatr.Queries.RouteQueries;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Logistics.Services.Mediatr.Handlers.RouteHandlers
{
    public class GetAllRoutesQueryHandler : IRequestHandler<GetAllRoutesQuery, IEnumerable<RouteDto>>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public GetAllRoutesQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<RouteDto>> Handle(GetAllRoutesQuery request, CancellationToken cancellationToken)
        {
            var route = await _repository.Routes.GetAllRoutesAsync(false);
            var routesDto = _mapper.Map<IEnumerable<RouteDto>>(route);
            return routesDto;
        }
    }
}
