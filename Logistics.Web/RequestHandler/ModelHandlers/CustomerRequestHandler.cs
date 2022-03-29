using RequestHandler.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class CustomerRequestHandler : ICustomerRequestHandler
    {
        private readonly string controllerUrl = "/api/Customers";

        private readonly IHttpClientFactoryHandler _httpClientHandler;
        public CustomerRequestHandler(IHttpClientFactoryHandler httpClientHandler)
        {
            _httpClientHandler = httpClientHandler;
        }

        public async Task<HttpResponseMessage> GetAllCustomers()
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl);
        }

        public async Task<HttpResponseMessage> GetCustomerById(int customerId)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl + $"/{customerId}");
        }

        public async Task<HttpResponseMessage> DeleteCustomerById(int customerId)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.DeleteAsync(controllerUrl + $"/{customerId}");
        }

        public async Task<HttpResponseMessage> PatchCustomerById(int customerId, HttpContent content)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.PatchAsync(controllerUrl + $"/{customerId}", content);
        }

        public async Task<HttpResponseMessage> CreateCustomer(HttpContent content)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.PostAsync(controllerUrl, content);
        }
    }
}
