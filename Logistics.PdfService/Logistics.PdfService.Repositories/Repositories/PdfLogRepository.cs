using Logistics.PdfService.Services.Interfaces;
using Logistics.PdfService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System;
using System.Linq;

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

        public async Task DeletePdfLog(int id)
        {
            using var con = _pdfLogsContext.CreateConnection();
            await con.QueryAsync($"Delete from PdfLogs where logid = {id}");
        }

        public async Task<IEnumerable<PdfLog>> GetAllPdfLogs()
        {
            using var con = _pdfLogsContext.CreateConnection();
            return await con.QueryAsync<PdfLog>($"Select * from PdfLogs");
        }

        public  async Task<PdfLog> GetPdfLogById(int id)
        {
            using var con = _pdfLogsContext.CreateConnection();
            var logs = await con.QueryAsync<PdfLog>($"Select * from PdfLogs where logid = {id}");
            return logs.FirstOrDefault();
        }
    }
}
