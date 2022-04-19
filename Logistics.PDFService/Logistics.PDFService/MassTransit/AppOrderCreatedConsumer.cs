using Logistics.Models.BrokerModels;
using Logistics.PdfService.Repositories.Interfaces;
using Logistics.PdfService.Services.Interfaces;
using MassTransit;
using System.Threading.Tasks;

namespace Logistics.PdfService.MassTransit
{
    public class AppOrderCreatedConsumer : IConsumer<CreatedOrderMessage>
    {
        private readonly IOrderPdfBuilder _orderPDFBuilder;
        private readonly IOrderPdfRepository _orderPdfRepository;
        private readonly IOrderPdfLogRepository _pdfLogRepository;
        private readonly IPdfLogService _pdfLogService;

        public AppOrderCreatedConsumer(IOrderPdfBuilder orderPDFBuilder, IOrderPdfRepository orderPdfRepository, IOrderPdfLogRepository pdfLogRepository, IPdfLogService pdfLogService)
        {
            _pdfLogRepository = pdfLogRepository;
            _orderPdfRepository = orderPdfRepository;
            _orderPDFBuilder = orderPDFBuilder;
            _pdfLogService = pdfLogService;
        }

        public async Task Consume(ConsumeContext<CreatedOrderMessage> context)
        {
            var orderMessage = context.Message;

            var createdPdf = await _orderPDFBuilder.BuildOrderPdf(orderMessage);
            await _orderPdfRepository.AddOrderPdf(createdPdf);
            var log = _pdfLogService.CreatePdfLog(orderMessage, createdPdf);
            await _pdfLogRepository.AddPdfLog(log);
        }
    }
}
