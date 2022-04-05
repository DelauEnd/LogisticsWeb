using Logistics.Models.ResponseDTO;
using Logistics.PdfService.Services.Interfaces;
using MassTransit;
using System.Threading.Tasks;

namespace Logistics.PdfService.MassTransit
{
    public class AppOrderCreatedConsumer : IConsumer<OrderDto>
    {
        private readonly IOrderPdfBuilder _orderPDFBuilder;
        private readonly IOrderPdfRepository _orderPdfRepository;
        private readonly IPdfLogRepository _pdfLogRepository;

        public AppOrderCreatedConsumer(IOrderPdfBuilder orderPDFBuilder, IOrderPdfRepository orderPdfRepository, IPdfLogRepository pdfLogRepository)
        {
            _pdfLogRepository = pdfLogRepository;
            _orderPdfRepository = orderPdfRepository;
            _orderPDFBuilder = orderPDFBuilder;
        }

        public async Task Consume(ConsumeContext<OrderDto> context)
        {
            var createdPdf = await _orderPDFBuilder.BuildOrderPdf(context.Message);
            await _orderPdfRepository.AddOrderPdf(createdPdf);
            await _pdfLogRepository.AddPdfLog(new Models.PdfLog
            {
                DocumentId = createdPdf.Id.ToString(),
                LogDate = System.DateTime.Now,
                OperationType = Models.Enum.OperationType.Added
            });
        }
    }
}
