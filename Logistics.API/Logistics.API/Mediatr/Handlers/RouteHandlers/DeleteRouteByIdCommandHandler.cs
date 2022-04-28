using AutoMapper;
using Logistics.API.Mediatr.Commands.RouteCommands;
using Logistics.Repository.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Logistics.API.Mediatr.Handlers.RouteHandlers
{
    public class DeleteRouteByIdCommandHandler : IRequestHandler<DeleteRouteByIdCommand>
    {
        private readonly IRepositoryManager _repository;
        public DeleteRouteByIdCommandHandler(IRepositoryManager repository)
        {
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
