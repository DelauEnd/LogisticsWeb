using Logistics.Models.BrokerModels;
using Logistics.Models.PdfModels;
using Logistics.PdfService.Models.Models;
using Logistics.PdfService.Services.Interfaces;

namespace Logistics.PdfService.Services.Services
{
    public class PdfLogService : IPdfLogService
    {
        public OrderPdfLog CreatePdfLog(CreatedOrderMessage createdOrderMessage, OrderPdf orderPdf)
        {
            var log = new OrderPdfLog
            {
                DocumentId = orderPdf.Id.ToString(),
                LogDate = System.DateTime.Now,
                OrderId = createdOrderMessage.Id,
                OrderRecieverAddress = createdOrderMessage.DestinationAddress,
                OrderRecieverSurname = createdOrderMessage.Destination.Surname,
                OrderSenderAddress = createdOrderMessage.SenderAddress,
                OrderSenderSurname = createdOrderMessage.Sender.Surname
            };
            return log;
        }
    }
}
