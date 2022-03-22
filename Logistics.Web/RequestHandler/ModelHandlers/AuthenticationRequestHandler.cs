using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RequestHandler.ModelHandlers
{
    public class AuthenticationRequestHandler : RequestHandlerBase
    {
        private readonly string controllerUrl = "/api/Authentication";

        public AuthenticationRequestHandler(IHttpClientService client) : base(client)
        {
        }

        public async Task<HttpResponseMessage> CreateUser(HttpContent content)
            => await HttpClient.Client.PostAsync(controllerUrl, content);

        public async Task<HttpResponseMessage> AuthenticateUser(HttpContent content)
            => await HttpClient.Client.PostAsync(controllerUrl + "/login", content);

        public async Task<HttpResponseMessage> AddRole(string userName, string role)
            => await HttpClient.Client.PostAsync(controllerUrl + $"/AddRole?login={userName}&role={role}", EmptyContent);


        public void InitUser(string token, IEnumerable<string> roles)
        {
            HttpClient.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpClient.Authenticated = true;
            HttpClient.UserRoles.AddRange(roles);
        }
    }
}
