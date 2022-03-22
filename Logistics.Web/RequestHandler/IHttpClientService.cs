using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace RequestHandler
{
    public interface IHttpClientService
    {
        public HttpClient Client { get; set; }
        public HttpClientHandler Handler { get; set; }
        public CookieContainer CookieContainer { get; set; }
        public bool Authenticated { get; set; }
        public List<string> UserRoles { get; }
    }
}
