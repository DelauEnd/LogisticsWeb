using Logistics.Models.ResponseDTO;
using Logistics.PdfService.Services.Interfaces;
using Logistics.PDFService.Services.Interfaces;
using MassTransit;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.PDFService.MassTransit
{
    public class AppOrderCreatedConsumer : IConsumer<OrderDto>
    {
        private readonly IOrderPdfGen _orderPDFGen;
        private readonly IOrderPdfRepository _orderPdfService;

        public AppOrderCreatedConsumer(IOrderPdfGen orderPDFGen, IOrderPdfRepository orderPdfService) : base()
        {
            _orderPdfService = orderPdfService;
            _orderPDFGen = orderPDFGen;
        }

        public async Task Consume(ConsumeContext<OrderDto> context)
        {
            await Task.Run(() => _orderPDFGen.GenOrderPdf(context.Message));
            var pdf = _orderPdfService.GetAllOrderPdfs().Result.FirstOrDefault();

            var res = BuildByteStr(pdf);
        }

        private static string BuildByteStr(Models.OrderPdf pdf)
        {
            var res = "";
            foreach (var num in pdf.PdfFile)
                res += num + ", ";
            return res;
        }
    }
}
