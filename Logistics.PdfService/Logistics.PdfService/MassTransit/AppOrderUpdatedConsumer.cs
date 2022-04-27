using Logistics.Models.BrokerModels;
using Logistics.Models.Enums;
using Logistics.PdfService.Repositories.Interfaces;
using Logistics.PdfService.Services.Interfaces;
using MassTransit;
using System.Threading.Tasks;

namespace Logistics.PdfService.MassTransit
{
    public class AppOrderUpdatedConsumer : IConsumer<UpdatedOrderMessage>
    {
        private readonly IOrderPdfBuilder _orderPDFBuilder;
        private readonly IOrderPdfRepository _orderPdfRepository;
        private readonly IOrderPdfLogRepository _pdfLogRepository;
        private readonly IPdfLogService _pdfLogService;

        public AppOrderUpdatedConsumer(IOrderPdfBuilder orderPDFBuilder, IOrderPdfRepository orderPdfRepository, IOrderPdfLogRepository pdfLogRepository, IPdfLogService pdfLogService)
        {
            _pdfLogRepository = pdfLogRepository;
            _orderPdfRepository = orderPdfRepository;
            _orderPDFBuilder = orderPDFBuilder;
            _pdfLogService = pdfLogService;
        }

        public async Task Consume(ConsumeContext<UpdatedOrderMessage> context)
        {
            var orderMessage = context.Message;
            
            var oldPdf = await _pdfLogRepository.GetPdfLogByOrderId(orderMessage.Id);
            var newPdf = await  _orderPDFBuilder.BuildOrderPdf(orderMessage);

            if(oldPdf == null)
                await _orderPdfRepository.AddOrderPdf(newPdf);
            else 
                await _orderPdfRepository.UpdateOrderPdf(oldPdf.DocumentId, newPdf);

            var log = _pdfLogService.CreatePdfLog(orderMessage, newPdf);
            log.OperationType = OperationType.Updated;
            await _pdfLogRepository.AddPdfLog(log);
        }
    }
}
