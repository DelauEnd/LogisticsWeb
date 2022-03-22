using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class CargoCategoriesRequestHandler : RequestHandlerBase
    {
        private readonly string controllerUrl = "/api/Categories";

        public CargoCategoriesRequestHandler(IHttpClientService client) : base(client)
        {
        }

        public async Task<HttpResponseMessage> GetAllCategories()
            => await HttpClient.Client.GetAsync(controllerUrl);

        public async Task<HttpResponseMessage> CreateCategory(HttpContent content)
            => await HttpClient.Client.PostAsync(controllerUrl, content);

        public async Task<HttpResponseMessage> DeleteCategoryById(int categoryId)
            => await HttpClient.Client.DeleteAsync(controllerUrl + $"/{categoryId}");

        public async Task<HttpResponseMessage> PutCategoryById(int categoryId, HttpContent content)
            => await HttpClient.Client.PutAsync(controllerUrl + $"/{categoryId}", content);
    }
}
