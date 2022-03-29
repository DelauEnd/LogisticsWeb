using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.Interfaces
{
    public interface IHttpClientFactoryHandler
    {
        public Task<HttpClient> GetAPIClient();
    }
}
