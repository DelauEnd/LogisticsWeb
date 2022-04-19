using Dapper;
using Logistics.PdfService.Models.Models;
using Logistics.PdfService.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.PdfService.Repositories.Repositories
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
                            PdfLogs (
                                logdate,
                                documentid,
                                orderid,
                                ordersendersurname,
                                ordersenderaddress,
                                orderrecieversurname,
                                orderrecieveraddress) 
                        VALUES 
                            (
                            @LogDate,
                            @DocumentId,
                            @OrderId,
                            @OrderSenderSurname,
                            @OrderSenderAddress,
                            @OrderRecieverSurname,
                            @OrderRecieverAddress)";

            var param = new
            {
                pdfLog.LogDate,
                pdfLog.DocumentId,
                pdfLog.OrderId,
                pdfLog.OrderSenderSurname,
                pdfLog.OrderSenderAddress,
                pdfLog.OrderRecieverSurname,
                pdfLog.OrderRecieverAddress
            };

            await _connection.QueryAsync(query, param);
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
