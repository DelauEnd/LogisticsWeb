using Logistics.PdfService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.PdfService.Services.Interfaces
{
    public interface IPdfLogRepository
    {
        public Task AddPdfLog(PdfLog pdfLog);
        public Task DeletePdfLog(int id);
        public Task<PdfLog> GetPdfLogById(int id);
        public Task<IEnumerable<PdfLog>> GetAllPdfLogs();
    }
}
