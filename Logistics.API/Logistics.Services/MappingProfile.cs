﻿using AutoMapper;
using Logistics.Entities.Enums;
using Logistics.Entities.Models;
using Logistics.Entities.Models.OwnedModels;
using Logistics.Models.BrokerModels;
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
            CreateRouteMaps();
            CreateOrderMaps();
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
