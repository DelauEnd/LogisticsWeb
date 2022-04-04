using Logistics.Models.ResponseDTO;
using System.Threading.Tasks;

namespace Logistics.PDFService.Interfaces
{
    public interface IOrderPDFGen
    {
        public Task GenOrderPDF(OrderDto order);
    }
}
