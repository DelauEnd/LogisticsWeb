using System.Net.Http;
using System.Text;

namespace RequestHandler.ModelHandlers
{
    public class RequestHandlerBase
    {
        protected IHttpClientService HttpClient { get; set; }

        public RequestHandlerBase(IHttpClientService client)
        {
            this.HttpClient = client;
        }

        public StringContent EmptyContent =>
            new StringContent("", Encoding.UTF8, "application/json");
    }
}
