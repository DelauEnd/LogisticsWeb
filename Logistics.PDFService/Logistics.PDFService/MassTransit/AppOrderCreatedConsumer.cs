using Logistics.Models.RequestDTO.CreateDTO;
using Logistics.Models.ResponseDTO;
using Logistics.PDFService.Interfaces;
using MassTransit;
using System.Threading.Tasks;

namespace Logistics.PDFService.MassTransit
{
    public class AppOrderCreatedConsumer : IConsumer<OrderDto>
    {
        private readonly IOrderPDFGen _orderPDFGen;

        public AppOrderCreatedConsumer(IOrderPDFGen orderPDFGen) :base()
        {
            _orderPDFGen = orderPDFGen;
        }

        public async Task Consume(ConsumeContext<OrderDto> context)
        {
            await _orderPDFGen.GenOrderPDF(context.Message);
        }
    }
}
