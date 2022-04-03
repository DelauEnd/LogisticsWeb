using Logistics.Models.ResponseDTO;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.PDFService.MassTransit
{
    public class AppOrderCreatedConsumerDefinitions : ConsumerDefinition<AppOrderCreatedConsumer>
    {
        public AppOrderCreatedConsumerDefinitions()
        {

        }
    }
}
