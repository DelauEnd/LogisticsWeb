using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class CustomerRequestHandler : RequestHandlerBase
    {
        private readonly string controllerUrl = "/api/Customers";

        public CustomerRequestHandler(IHttpClientService client) : base(client)
        {
        }

        public async Task<HttpResponseMessage> GetAllCustomers()
            => await HttpClient.Client.GetAsync(controllerUrl);

        public async Task<HttpResponseMessage> GetCustomerById(int customerId)
            => await HttpClient.Client.GetAsync(controllerUrl + $"/{customerId}");

        public async Task<HttpResponseMessage> DeleteCustomerById(int customerId)
            => await HttpClient.Client.DeleteAsync(controllerUrl + $"/{customerId}");

        public async Task<HttpResponseMessage> PatchCustomerById(int customerId, HttpContent content)
            => await HttpClient.Client.PatchAsync(controllerUrl + $"/{customerId}", content);

        public async Task<HttpResponseMessage> CreateCustomer(HttpContent content)
               => await HttpClient.Client.PostAsync(controllerUrl, content);
    }
}
