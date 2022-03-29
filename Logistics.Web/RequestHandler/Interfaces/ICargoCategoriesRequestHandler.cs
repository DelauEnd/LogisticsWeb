using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.Interfaces
{
    public interface ICargoCategoriesRequestHandler
    {
        public Task<HttpResponseMessage> GetAllCategories();
        public Task<HttpResponseMessage> CreateCategory(HttpContent content);
        public Task<HttpResponseMessage> DeleteCategoryById(int categoryId);
        public Task<HttpResponseMessage> PutCategoryById(int categoryId, HttpContent content);
    }
}
