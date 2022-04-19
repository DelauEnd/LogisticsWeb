using Logistics.PdfService.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.PdfService.Repositories.Interfaces
{
    public interface IOrderPdfRepository
    {
        public Task AddOrderPdf(OrderPdf orderPdf);
        public Task DeleteOrderPdf(string id);
        public Task<OrderPdf> GetOrderPdfById(string id);
        public Task<IEnumerable<OrderPdf>> GetAllOrderPdfs();
        public Task UpdateOrderPdf(string id, OrderPdf orderPdf);
    }
}
