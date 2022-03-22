using AutoMapper;
using Logistics.Entities.Enums;
using Logistics.Entities.Models;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using System;

namespace Logistics.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateTransportMaps();
            CreateCargoMaps();
            CreateRouteMaps();
            CreateOrderMaps();
            CreateCustomerMaps();
            CreateCargoCategoryMaps();
            CreateUserMaps();
            CreateOwnedModelsMaps();
        }

        private void CreateOwnedModelsMaps()
        {
            CreateMap<Models.OwnedModels.Person, Person>().ReverseMap();
            CreateMap<Models.OwnedModels.Dimensions, Dimensions>().ReverseMap();
        }

        private void CreateUserMaps()
        {
            CreateMap<UserForCreationDto, User>();
        }

        private void CreateTransportMaps()
        {
            CreateMap<Transport, TransportDto>();

            CreateMap<TransportForCreationDto, Transport>();

            CreateMap<TransportForUpdateDto, Transport>().ReverseMap();
        }

        private void CreateCargoMaps()
        {
            CreateMap<Cargo, CargoDto>()
                .ForMember(cargoDto => cargoDto.Category, option =>
                option.MapFrom(cargo =>
                cargo.Category.Title));

            CreateMap<CargoForUpdateDto, Cargo>().ReverseMap();

            CreateMap<CargoForCreationDto, Cargo>();
        }

        private void CreateRouteMaps()
        {
            CreateMap<Route, RouteDto>()
                .ForMember(routeDto => routeDto.TransportRegistrationNumber, option =>
                option.MapFrom(transport =>
                transport.Transport.RegistrationNumber));
        }

        private void CreateOrderMaps()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(orderDto => orderDto.Sender, option =>
                option.MapFrom(order => order.Sender.Address))
                .ForMember(orderDto => orderDto.Destination, option =>
                option.MapFrom(order => order.Destination.Address));

            CreateMap<OrderForCreationDto, Order>()
                .ForMember(order => order.Status, option =>
                option.MapFrom(orderForCreation => Status.Processing));

            CreateMap<OrderForUpdateDto, Order>()
                .ForMember(order => order.Status, option =>
                option.MapFrom(order =>
                    Enum.IsDefined(typeof(Status), order.Status) ?
                    Enum.Parse(typeof(Status), order.Status) :
                    Status.Processing))
                .ReverseMap()
                .ForMember(updateOrder => updateOrder.Status, option =>
                option.MapFrom(order => order.Status.ToString()));
        }

        private void CreateCustomerMaps()
        {
            CreateMap<Customer, CustomerDto>();

            CreateMap<CustomerForCreationDto, Customer>();

            CreateMap<CustomerForUpdateDto, Customer>().ReverseMap();
        }

        private void CreateCargoCategoryMaps()
        {
            CreateMap<CargoCategory, CargoCategoryDto>();

            CreateMap<CategoryForCreationDto, CargoCategory>();

            CreateMap<CargoCategoryForUpdateDto, CargoCategory>().ReverseMap();
        }
    }
}
