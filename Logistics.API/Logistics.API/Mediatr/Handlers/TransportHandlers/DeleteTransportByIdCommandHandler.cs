using AutoMapper;
using Logistics.API.Mediatr.Commands.TransportCommands;
using Logistics.Repository.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Logistics.API.Mediatr.Handlers.TransportHandlers
{
    public class DeleteTransportByIdCommandHandler : IRequestHandler<DeleteTransportByIdCommand>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public DeleteTransportByIdCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteTransportByIdCommand request, CancellationToken cancellationToken)
        {
            var transport = await _repository.Transport.GetTransportByIdAsync(request.TransportId, false);
            _repository.Transport.DeleteTransport(transport);
            await _repository.SaveAsync();

            return Unit.Value;
        }
    }
}
