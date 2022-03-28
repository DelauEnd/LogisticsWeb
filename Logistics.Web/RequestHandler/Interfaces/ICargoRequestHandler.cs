using System.Net.Http;
using System.Threading.Tasks;

namespace RequestHandler.Interfaces
{
    public interface ICargoRequestHandler
    {
        public Task<HttpResponseMessage> GetAllCargoes();
    }
}
