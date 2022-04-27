using Logistics.Models.BrokerModels;
using Logistics.PdfService.Models.Models;
using System.Threading.Tasks;

namespace Logistics.PdfService.Services.Interfaces
{
    public interface IOrderPdfBuilder
    {
        public Task<OrderPdf> BuildOrderPdf(OrderMessageBase order);
    }
}
