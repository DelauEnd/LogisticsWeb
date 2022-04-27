using Logistics.Models.PdfModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.PdfService.Repositories.Interfaces
{
    public interface IOrderPdfLogRepository
    {
        public Task AddPdfLog(OrderPdfLog pdfLog);
        public Task DeletePdfLog(int id);
        public Task<OrderPdfLog> GetPdfLogByOrderId(int id);
        public Task<IEnumerable<OrderPdfLog>> GetAllPdfLogs();
    }
}
