using AutoMapper;
using Logistics.API.Mediatr.Commands.TransportCommands;
using Logistics.Entities.Models;
using Logistics.Models.ResponseDTO;
using Logistics.Repository.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Logistics.API.Mediatr.Handlers.TransportHandlers
{
    public class AddTransportCommandHandler : IRequestHandler<AddTransportCommand, TransportDto>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public AddTransportCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<TransportDto> Handle(AddTransportCommand request, CancellationToken cancellationToken)
        {
            var transport = _mapper.Map<Transport>(request.TransportForCreation);
            _repository.Transport.CreateTransport(transport);
            await _repository.SaveAsync();

            return _mapper.Map<TransportDto>(transport);
        }
    }
}
