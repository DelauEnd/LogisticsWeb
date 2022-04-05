using Logistics.PdfService.Services.Interfaces;
using Logistics.PdfService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace Logistics.PdfService.Repositories
{
    public class PdfLogRepository : IPdfLogRepository
    {
        private readonly PdfLogsContext _pdfLogsContext;
        public PdfLogRepository(PdfLogsContext pdfLogsContext)
        {
            _pdfLogsContext = pdfLogsContext;
        }

        public async Task AddPdfLog(PdfLog pdfLog)
        {
            using var con = _pdfLogsContext.CreateConnection();
            await con.QueryAsync(@"Insert into PdfLogs Values @LogDate, @DocumentId, @OperationType", new { pdfLog.LogDate, pdfLog.DocumentId, OperationType = nameof(pdfLog.OperationType)});
        }

        public Task DeletePdfLog(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<PdfLog>> GetAllPdfLogs()
        {
            throw new System.NotImplementedException();
        }

        public Task<PdfLog> GetPdfLogById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
