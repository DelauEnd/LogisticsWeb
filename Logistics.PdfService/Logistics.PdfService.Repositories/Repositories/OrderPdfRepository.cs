using Logistics.PdfService.Services.Interfaces;
using Logistics.PdfService.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logistics.PdfService.Repositories
{
    public class OrderPdfRepository : IOrderPdfRepository
    {
        private readonly IMongoCollection<OrderPdf> _orderPdfCollection;

        public OrderPdfRepository(IConfiguration configuration)
        {
            var pdfStoreSection = configuration.GetSection("PdfStoreDatabase");

            var conStr = pdfStoreSection.GetSection("ConnectionString").Value;
            var DBName = pdfStoreSection.GetSection("DatabaseName").Value;
            var orderCollectionName = pdfStoreSection.GetSection("OrderCollectionName").Value;

            var mongoClient = new MongoClient(conStr);
            var mongoDB = mongoClient.GetDatabase(DBName);

            _orderPdfCollection = mongoDB.GetCollection<OrderPdf>(orderCollectionName);
        }

        public async Task AddOrderPdf(OrderPdf orderPdf)
        {
            await _orderPdfCollection.InsertOneAsync(orderPdf);
        }

        public async Task DeleteOrderPdf(string id)
        {
            await _orderPdfCollection.DeleteOneAsync(id);
        }

        public async Task<IEnumerable<OrderPdf>> GetAllOrderPdfs()
        {
            return await _orderPdfCollection.Find(x => true).ToListAsync();
        }

        public async Task<OrderPdf> GetOrderPdfById(string id)
        {
            return await _orderPdfCollection.Find(x => x.Id.ToString() == id).FirstOrDefaultAsync();
        }

        public async Task UpdateOrderPdf(string id, OrderPdf orderPdf)
        {
            await _orderPdfCollection.ReplaceOneAsync(x => x.Id.ToString() == id, orderPdf);
        }
    }
}
