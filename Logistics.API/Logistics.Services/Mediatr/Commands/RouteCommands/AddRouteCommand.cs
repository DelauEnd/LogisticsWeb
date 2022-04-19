﻿using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.ResponseDTO;
using MediatR;

namespace Logistics.Services.Mediatr.Commands.RouteCommands
{
    public class AddRouteCommand : IRequest<RouteDto>
    {
        public RouteForCreationDto RouteForCreation { get; }

        public AddRouteCommand(RouteForCreationDto routeForCreation)
        {
            RouteForCreation = routeForCreation;
        }
    }
}
