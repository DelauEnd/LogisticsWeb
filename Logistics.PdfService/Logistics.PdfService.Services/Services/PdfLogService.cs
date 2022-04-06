using Logistics.Models.BrokerModels;
using Logistics.PdfService.Models;
using Logistics.PdfService.Services.Interfaces;

namespace Logistics.PdfService.Services
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
                OrderSenderSurname = createdOrderMessage.Sender.Surname,
                OperationType = Models.Enum.OperationType.Added
            };
            return log;
        }
    }
}
