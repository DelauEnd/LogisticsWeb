using Logistics.PdfService.Services.Interfaces;
using Logistics.PdfService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using System.Data;

namespace Logistics.PdfService.Repositories
{
    public class OrderPdfLogRepository : IOrderPdfLogRepository
    {
        private readonly IDbConnection _connection;
        public OrderPdfLogRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task AddPdfLog(OrderPdfLog pdfLog)
        {
            var query = $@"
                        INSERT INTO
                            PdfLogs (logdate, documentid, orderid, ordersendersurname, ordersenderaddress, orderrecieversurname, orderrecieveraddress, operationtype) 
                        VALUES 
                            (@LogDate, @DocumentId, @OrderId, @OrderSenderSurname, @OrderSenderAddress, @OrderRecieverSurname, @OrderRecieverAddress, '{pdfLog.OperationType}')";

            await _connection.QueryAsync(query, new { pdfLog.LogDate, pdfLog.DocumentId, pdfLog.OrderId, pdfLog.OrderSenderSurname, pdfLog.OrderSenderAddress, pdfLog.OrderRecieverSurname, pdfLog.OrderRecieverAddress });
        }

        public async Task DeletePdfLog(int id)
        {
            var query = $@"
                        DELETE FROM
                            PdfLogs
                        WHERE
                            logid = {id}";

            await _connection.QueryAsync(query);
        }

        public async Task<IEnumerable<OrderPdfLog>> GetAllPdfLogs()
        {
            var query = $@"
                        SELECT
                            *
                        FROM
                            PdfLogs";

            return await _connection.QueryAsync<OrderPdfLog>(query);
        }

        public async Task<OrderPdfLog> GetPdfLogById(int id)
        {
            var query = @$"
                        SELECT 
                            *
                        FROM
                            PdfLogs
                        WHERE
                            logid = {id}";

            var logs = await _connection.QueryAsync<OrderPdfLog>(query);
            return logs.FirstOrDefault();
        }
    }
}
