using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.Interfaces
{
    public interface ICustomerRequestHandler
    {
        public Task<HttpResponseMessage> GetAllCustomers();
        public Task<HttpResponseMessage> GetCustomerById(int customerId);
        public Task<HttpResponseMessage> DeleteCustomerById(int customerId);
        public Task<HttpResponseMessage> PatchCustomerById(int customerId, HttpContent content);
        public Task<HttpResponseMessage> CreateCustomer(HttpContent content);
    }
}
