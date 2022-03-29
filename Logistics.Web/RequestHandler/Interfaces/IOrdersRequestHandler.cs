using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.Interfaces
{
    public interface IOrderRequestHandler
    {
        public Task<HttpResponseMessage> GetAllOrders();
        public Task<HttpResponseMessage> GetOrderById(int orderId);
        public Task<HttpResponseMessage> DeleteOrderById(int orderId);
        public Task<HttpResponseMessage> PatchOrderById(int orderId, HttpContent content);
        public Task<HttpResponseMessage> CreateOrder(HttpContent content);
        public Task<HttpResponseMessage> CreateCargoForOrder(int orderId, HttpContent content);
        public Task<HttpResponseMessage> GetCargoesForOrder(int orderId);
    }
}
