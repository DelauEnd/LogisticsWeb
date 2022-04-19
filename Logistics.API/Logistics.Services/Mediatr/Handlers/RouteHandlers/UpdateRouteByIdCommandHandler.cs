using AutoMapper;
using Logistics.Models.ResponseDTO;
using Logistics.Repository.Interfaces;
using Logistics.Services.Mediatr.Commands.RouteCommands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Logistics.Services.Mediatr.Handlers.RouteHandlers
{
    public class UpdateRouteByIdCommandHandler : IRequestHandler<UpdateRouteByIdCommand, RouteDto>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public UpdateRouteByIdCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<RouteDto> Handle(UpdateRouteByIdCommand request, CancellationToken cancellationToken)
        {
            var routeToUpdate = await _repository.Routes.GetRouteByIdAsync(request.RouteId, false);

            var transport = await _repository.Transport.GetTransportByRegistrationNumberAsync(request.RouteForUpdate.TransportRegistrationNumber, false); ;
            routeToUpdate.TransportId = transport.Id;
            await _repository.SaveAsync();

            routeToUpdate.Transport = transport;
            return _mapper.Map<RouteDto>(routeToUpdate);
        }
    }
}
