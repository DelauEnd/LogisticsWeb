using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace RequestHandler
{
    public class HttpClientService : IHttpClientService
    {
        public HttpClient Client { get; set; }
        public HttpClientHandler Handler { get; set; }
        public CookieContainer CookieContainer { get; set; }
        public bool Authenticated { get; set; }
        public List<string> UserRoles { get; private set; }

        public HttpClientService()
        {
            Handler = new HttpClientHandler()
            {
                CookieContainer = new CookieContainer()
            };
            Client = new HttpClient(Handler);

            Authenticated = false;

            UserRoles = new List<string>();
        }
    }
}
