using Logistics.Models.ResponseDTO;
using System.Threading.Tasks;

namespace Logistics.PDFService.Services.Interfaces
{
    public interface IOrderPdfGen
    {
        public Task GenOrderPdf(OrderDto order);
    }
}
