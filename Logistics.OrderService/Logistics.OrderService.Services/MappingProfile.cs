using AutoMapper;
using Logistics.Entities.Enums;
using Logistics.Entities.Models;
using Logistics.Entities.Models.OwnedModels;
using Logistics.Models.BrokerModels;
using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.RequestDTO.UpdateDTO;
using Logistics.Models.ResponseDTO;
using System;

namespace Logistics.OrderService.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateCargoMaps();
            CreateOrderMaps();
            CreateOwnedModelsMaps();
            CreateCustomerMaps();
            CreateCargoCategoryMaps();
        }

        private void CreateCargoCategoryMaps()
        {
            CreateMap<CargoCategory, CargoCategoryDto>();

            CreateMap<CategoryForCreationDto, CargoCategory>();

            CreateMap<CargoCategoryForUpdateDto, CargoCategory>().ReverseMap();
        }

        private void CreateCustomerMaps()
        {
            CreateMap<Customer, CustomerDto>();

            CreateMap<CustomerForCreationDto, Customer>();

            CreateMap<CustomerForUpdateDto, Customer>().ReverseMap();
        }

        private void CreateOwnedModelsMaps()
        {
            CreateMap<Models.OwnedModels.Person, Person>().ReverseMap();
            CreateMap<Models.OwnedModels.Dimensions, Dimensions>().ReverseMap();
        }

        private void CreateCargoMaps()
        {
            CreateMap<Cargo, CargoDto>()
                .ForMember(cargoDto => cargoDto.Category, option =>
                option.MapFrom(cargo =>
                cargo.Category.Title));

            CreateMap<CargoForUpdateDto, Cargo>().ReverseMap();

            CreateMap<CargoForCreationDto, Cargo>();

            CreateMap<Cargo, CargoForOrderMessage>();
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

            CreateMap<Order, CreatedOrderMessage>()
                .ForMember(orderMessage => orderMessage.SenderAddress, option =>
                option.MapFrom(order => order.Sender.Address))
                .ForMember(orderMessage => orderMessage.Sender, option =>
                option.MapFrom(order => order.Sender.ContactPerson))
                .ForMember(orderMessage => orderMessage.DestinationAddress, option =>
                option.MapFrom(order => order.Destination.Address))
                .ForMember(orderMessage => orderMessage.Destination, option =>
                option.MapFrom(order => order.Destination.ContactPerson))
                .ForMember(orderMessage => orderMessage.Cargoes, option =>
                option.MapFrom(order => order.Cargoes));
        }
    }
}
