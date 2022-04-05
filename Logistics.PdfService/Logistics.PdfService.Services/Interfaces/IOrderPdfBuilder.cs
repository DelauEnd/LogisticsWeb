using Logistics.Models.ResponseDTO;
using Logistics.PdfService.Models;
using System.Threading.Tasks;

namespace Logistics.PdfService.Services.Interfaces
{
    public interface IOrderPdfBuilder
    {
        public Task<OrderPdf> BuildOrderPdf(OrderDto order);
    }
}
