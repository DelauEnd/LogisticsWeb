﻿using MediatR;

namespace Logistics.Services.Mediatr.Commands.TransportCommands
{
    public class DeleteTransportByIdCommand : IRequest
    {
        public int TransportId { get; }
        public DeleteTransportByIdCommand(int transportId)
        {
            TransportId = transportId;
        }
    }
}
