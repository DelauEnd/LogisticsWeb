using Logistics.PdfService.Services.Interfaces;
using Logistics.PdfService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace Logistics.PdfService.Repositories
{
    public class OrderPdfLogRepository : IOrderPdfLogRepository
    {
        private readonly PdfLogsContext _pdfLogsContext;
        public OrderPdfLogRepository(PdfLogsContext pdfLogsContext)
        {
            _pdfLogsContext = pdfLogsContext;
        }

        public async Task AddPdfLog(OrderPdfLog pdfLog)
        {
            using var con = _pdfLogsContext.CreateConnection();
            await con.QueryAsync(@$"Insert into PdfLogs (logdate, documentid, orderid, ordersendersurname, ordersenderaddress, orderrecieversurname, orderrecieveraddress, operationtype) 
                Values (@LogDate, @DocumentId, @OrderId, @OrderSenderSurname, @OrderSenderAddress, @OrderRecieverSurname, @OrderRecieverAddress, '{pdfLog.OperationType}')",
                new { pdfLog.LogDate, pdfLog.DocumentId, pdfLog.OrderId, pdfLog.OrderSenderSurname, pdfLog.OrderSenderAddress, pdfLog.OrderRecieverSurname, pdfLog.OrderRecieverAddress });
        }

        public async Task DeletePdfLog(int id)
        {
            using var con = _pdfLogsContext.CreateConnection();
            await con.QueryAsync($"Delete from PdfLogs where logid = {id}");
        }

        public async Task<IEnumerable<OrderPdfLog>> GetAllPdfLogs()
        {
            using var con = _pdfLogsContext.CreateConnection();
            return await con.QueryAsync<OrderPdfLog>($"Select * from PdfLogs");
        }

        public  async Task<OrderPdfLog> GetPdfLogById(int id)
        {
            using var con = _pdfLogsContext.CreateConnection();
            var logs = await con.QueryAsync<OrderPdfLog>($"Select * from PdfLogs where logid = {id}");
            return logs.FirstOrDefault();
        }
    }
}
