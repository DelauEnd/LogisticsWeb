using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.ResponseDTO;
using MassTransit;
using System.Threading.Tasks;

namespace Logistics.PDFService.MassTransit
{
    public class AppOrderCreatedConsumer : IConsumer<OrderDto>
    {
        public Task Consume(ConsumeContext<OrderDto> context)
        {


            return Task.CompletedTask;
        }
    }
}
