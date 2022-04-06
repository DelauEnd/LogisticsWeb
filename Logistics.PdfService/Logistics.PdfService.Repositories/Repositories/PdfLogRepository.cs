using Logistics.PdfService.Services.Interfaces;
using Logistics.PdfService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System;

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
            await con.QueryAsync($"Insert into PdfLogs (logdate, documentid, operationtype) Values (@LogDate, @DocumentId, '{pdfLog.OperationType}')", new { pdfLog.LogDate, pdfLog.DocumentId });
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
