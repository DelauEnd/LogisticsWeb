using Logistics.Models.ResponseDTO;
using System.Threading.Tasks;

namespace Logistics.PDFService.Interfaces
{
    public interface IOrderPDFGen
    {
        public string GenOrderPDF(OrderDto order);
    }
}
