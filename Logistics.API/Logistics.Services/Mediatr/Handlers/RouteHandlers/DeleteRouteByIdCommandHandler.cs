using AutoMapper;
using Logistics.Repository.Interfaces;
using Logistics.Services.Mediatr.Commands.RouteCommands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Logistics.Services.Mediatr.Handlers.RouteHandlers
{
    public class DeleteRouteByIdCommandHandler : IRequestHandler<DeleteRouteByIdCommand>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public DeleteRouteByIdCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteRouteByIdCommand request, CancellationToken cancellationToken)
        {
            var route = await _repository.Routes.GetRouteByIdAsync(request.RouteId, false);
            _repository.Routes.DeleteRoute(route);
            await _repository.SaveAsync();

            return Unit.Value;
        }
    }
}
