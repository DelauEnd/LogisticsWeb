using Logistics.Models.ResponseDTO;
using Logistics.PDFService.Interfaces;
using System.Threading.Tasks;

namespace Logistics.PDFService.Services
{
    public class OrderPDFGen : IOrderPDFGen
    {
        public async Task GenOrderPDF(OrderDto order)
        {
            return;
        }
    }
}
