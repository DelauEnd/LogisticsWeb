using Logistics.PdfService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.PdfService.Services.Interfaces
{
    public interface IOrderPdfLogRepository
    {
        public Task AddPdfLog(OrderPdfLog pdfLog);
        public Task DeletePdfLog(int id);
        public Task<OrderPdfLog> GetPdfLogById(int id);
        public Task<IEnumerable<OrderPdfLog>> GetAllPdfLogs();
    }
}
