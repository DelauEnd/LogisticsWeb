using AutoMapper;
using Logistics.Entities.Models;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using Logistics.Repository.Interfaces;
using Logistics.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.Services.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public RouteService(IRepositoryManager repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AddRoute(RouteForCreationDto routeToAdd)
        {
            var transport = await _repository.Transport.GetTransportByRegistrationNumberAsync(routeToAdd.TransportRegistrationNumber, false);

            Route route = new Route
            {
                TransportId = transport.Id,
            };

            _repository.Routes.CreateRoute(route);
            await _repository.SaveAsync();
        }

        public async Task AssignCargoesToRoute(List<int> ids, int routeId)
        {
            var route = await _repository.Routes.GetRouteByIdAsync(routeId, false);

            foreach (var id in ids)
            {
                if (await _repository.Cargoes.GetCargoByIdAsync(id, false) == null)
                    return;
                await _repository.Cargoes.AssignCargoToRoute(id, route.Id);
            }
            await _repository.SaveAsync();
        }

        public async Task DeleteRouteById(int routeId)
        {
            var route = await _repository.Routes.GetRouteByIdAsync(routeId, false);
            _repository.Routes.DeleteRoute(route);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<RouteDto>> GetAllRoutes()
        {
            var route = await _repository.Routes.GetAllRoutesAsync(false);
            var routesDto = _mapper.Map<IEnumerable<RouteDto>>(route);
            return routesDto;
        }

        public async Task<IEnumerable<CargoDto>> GetCargoesByRouteId(int routeId)
        {
            var route = await _repository.Routes.GetRouteByIdAsync(routeId, false);
            var cargoes = await _repository.Cargoes.GetCargoesByRouteIdAsync(route.Id, false);

            var cargoesDto = _mapper.Map<IEnumerable<CargoDto>>(cargoes);
            return cargoesDto;
        }

        public async Task<RouteDto> GetRouteById(int routeId)
        {
            var route = await _repository.Routes.GetRouteByIdAsync(routeId, false);
            var routeDto = _mapper.Map<RouteDto>(route);
            return routeDto;
        }

        public async Task UpdateRouteById(int routeId, RouteForUpdateDto route)
        {
            var routeToUpdate = await _repository.Routes.GetRouteByIdAsync(routeId, false);

            var transport = await _repository.Transport.GetTransportByRegistrationNumberAsync(route.TransportRegistrationNumber, false); ;
            routeToUpdate.TransportId = transport.Id;
            await _repository.SaveAsync();
        }
    }
}
