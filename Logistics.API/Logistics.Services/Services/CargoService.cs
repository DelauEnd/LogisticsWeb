using AutoMapper;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Logistics.Repository.Interfaces;
using Logistics.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Services.Services
{
    public class CargoService : ICargoService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public CargoService(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task DeleteCargoById(int cargoId)
        {
            var cargo = await _repository.Cargoes.GetCargoByIdAsync(cargoId, false);
            _repository.Cargoes.DeleteCargo(cargo);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<CargoDto>> GetAllCargoes()
        {
            var cargoes = await _repository.Cargoes.GetAllCargoesAsync(false);
            var cargoesDto = _mapper.Map<IEnumerable<CargoDto>>(cargoes);
            return cargoesDto;
        }

        public async Task<CargoDto> GetCargoById(int cargoId)
        {
            var cargo = await _repository.Cargoes.GetCargoByIdAsync(cargoId, false);
            var cargoDto = _mapper.Map<CargoDto>(cargo);
            return cargoDto;
        }

        public async Task<IEnumerable<CargoDto>> GetUnassignedCargoes()
        {
            var cargoes = await _repository.Cargoes.GetUnassignedCargoesAsync(false);
            var cargoesDto = _mapper.Map<IEnumerable<CargoDto>>(cargoes);
            return cargoesDto;
        }

        public async Task PatchCargoById(int cargoId, JsonPatchDocument<CargoForUpdateDto> patchDoc)
        {
            var cargo = await _repository.Cargoes.GetCargoByIdAsync(cargoId, false);
            var cargoToPatch = _mapper.Map<CargoForUpdateDto>(cargo);
            patchDoc.ApplyTo(cargoToPatch);
            _mapper.Map(cargoToPatch, cargo);

            await _repository.SaveAsync();
        }
    }
}
