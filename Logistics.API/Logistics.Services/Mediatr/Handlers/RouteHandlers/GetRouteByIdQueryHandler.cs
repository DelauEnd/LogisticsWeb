using AutoMapper;
using Logistics.Models.ResponseDTO;
using Logistics.Repository.Interfaces;
using Logistics.Services.Mediatr.Queries.RouteQueries;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Logistics.Services.Mediatr.Handlers.RouteHandlers
{
    public class GetRouteByIdQueryHandler : IRequestHandler<GetRouteByIdQuery, RouteDto>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public GetRouteByIdQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<RouteDto> Handle(GetRouteByIdQuery request, CancellationToken cancellationToken)
        {
            var route = await _repository.Routes.GetRouteByIdAsync(request.RouteId, false);
            var routeDto = _mapper.Map<RouteDto>(route);
            return routeDto;
        }
    }
}
