using RequestHandler.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class CargoCategoriesRequestHandler : ICargoCategoriesRequestHandler
    {
        private readonly string controllerUrl = "/api/Categories";

        private readonly IHttpClientFactoryHandler _httpClientHandler;
        public CargoCategoriesRequestHandler(IHttpClientFactoryHandler httpClientHandler)
        {
            _httpClientHandler = httpClientHandler;
        }

        public async Task<HttpResponseMessage> GetAllCategories()
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.GetAsync(controllerUrl);
        }

        public async Task<HttpResponseMessage> CreateCategory(HttpContent content)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.PostAsync(controllerUrl, content);
        }

        public async Task<HttpResponseMessage> DeleteCategoryById(int categoryId)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.DeleteAsync(controllerUrl + $"/{categoryId}");
        }

        public async Task<HttpResponseMessage> PutCategoryById(int categoryId, HttpContent content)
        {
            using HttpClient client = await _httpClientHandler.GetAPIClient();
            return await client.PutAsync(controllerUrl + $"/{categoryId}", content);
        }
    }
}
