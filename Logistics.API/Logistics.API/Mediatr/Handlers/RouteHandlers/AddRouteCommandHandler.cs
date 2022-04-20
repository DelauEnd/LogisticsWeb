using AutoMapper;
using Logistics.API.Mediatr.Commands.RouteCommands;
using Logistics.Entities.Models;
using Logistics.Models.ResponseDTO;
using Logistics.Repository.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Logistics.API.Mediatr.Handlers.RouteHandlers
{
    public class AddRouteCommandHandler : IRequestHandler<AddRouteCommand, RouteDto>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public AddRouteCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<RouteDto> Handle(AddRouteCommand request, CancellationToken cancellationToken)
        {
            var transport = await _repository.Transport.GetTransportByRegistrationNumberAsync(request.RouteForCreation.TransportRegistrationNumber, false);

            Route route = new Route
            {
                TransportId = transport.Id,
            };

            _repository.Routes.CreateRoute(route);
            await _repository.SaveAsync();

            route.Transport = transport;
            return _mapper.Map<RouteDto>(route);
        }
    }
}
