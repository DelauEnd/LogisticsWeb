using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.ResponseDTO;
using MassTransit;
using System.Threading.Tasks;

namespace Logistics.PDFService.MassTransit
{
    public class AppOrderCreatedConsumer : IConsumer<OrderForCreationDto>
    {
        public Task Consume(ConsumeContext<OrderForCreationDto> context)
        {


            return Task.CompletedTask;
        }
    }
}
