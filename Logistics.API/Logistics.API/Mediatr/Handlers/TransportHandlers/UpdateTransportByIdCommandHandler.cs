using AutoMapper;
using Logistics.API.Mediatr.Commands.TransportCommands;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Logistics.Repository.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Logistics.API.Mediatr.Handlers.TransportHandlers
{
    public class UpdateTransportByIdCommandHandler : IRequestHandler<UpdateTransportByIdCommand, TransportDto>
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public UpdateTransportByIdCommandHandler(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<TransportDto> Handle(UpdateTransportByIdCommand request, CancellationToken cancellationToken)
        {
            var transport = await _repository.Transport.GetTransportByIdAsync(request.TransportId, false);
            var transporToPatch = _mapper.Map<TransportForUpdateDto>(transport);

            request.TransportPatchDoc.ApplyTo(transporToPatch);
            _mapper.Map(transporToPatch, transport);

            await _repository.SaveAsync();

            return _mapper.Map<TransportDto>(transport);
        }
    }
}
