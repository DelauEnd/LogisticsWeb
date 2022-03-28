using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.Interfaces
{
    public interface ICargoCategoriesRequestHandler
    {
        public Task<HttpResponseMessage> GetAllCategories();
    }
}
