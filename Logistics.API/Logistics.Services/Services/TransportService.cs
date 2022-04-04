using AutoMapper;
using Logistics.Entities.Models;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Logistics.Repository.Interfaces;
using Logistics.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Services.Services
{
    public class TransportService : ITransportService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public TransportService(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<TransportDto> AddTransport(TransportForCreationDto transportToAdd)
        {
            var transport = _mapper.Map<Transport>(transportToAdd);
            _repository.Transport.CreateTransport(transport);
            await _repository.SaveAsync();

            return _mapper.Map<TransportDto>(transport);
        }

        public async Task DeleteTransportById(int transportId)
        {
            var transport = await _repository.Transport.GetTransportByIdAsync(transportId, false);
            _repository.Transport.DeleteTransport(transport);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<TransportDto>> GetAllTransport()
        {
            var transpor = await _repository.Transport.GetAllTransportAsync(false);
            var transportDto = _mapper.Map<IEnumerable<TransportDto>>(transpor);
            return transportDto;
        }

        public async Task<TransportDto> GetTransportById(int transportId)
        {
            var transport = await _repository.Transport.GetTransportByIdAsync(transportId, false);
            var transportDto = _mapper.Map<TransportDto>(transport);
            return transportDto;
        }

        public async Task<TransportDto> PatchTransportById(int transportId, JsonPatchDocument<TransportForUpdateDto> patchDoc)
        {
            var transport = await _repository.Transport.GetTransportByIdAsync(transportId, false);
            var transporToPatch = _mapper.Map<TransportForUpdateDto>(transport);

            patchDoc.ApplyTo(transporToPatch);
            _mapper.Map(transporToPatch, transport);

            await _repository.SaveAsync();

            return _mapper.Map<TransportDto>(transport);
        }
    }
}
