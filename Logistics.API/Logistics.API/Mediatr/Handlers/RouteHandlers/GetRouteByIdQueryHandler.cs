using AutoMapper;
using Logistics.API.Mediatr.Queries.RouteQueries;
using Logistics.Models.ResponseDTO;
using Logistics.Repository.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Logistics.API.Mediatr.Handlers.RouteHandlers
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
