using Microsoft.Extensions.Configuration;
using RequestHandler.ModelHandlers;
using System;
using System.Collections.Generic;

namespace RequestHandler
{
    public class RequestManager : IRequestManager
    {
        public IHttpClientService HttpClient { get; private set; }
        private readonly IConfiguration configuration;
        private string BaseUrl
            => configuration.GetSection("ApiBaseUrl").Value;

        public RequestManager(IConfiguration configuration, IHttpClientService client)
        {
            this.configuration = configuration;
            HttpClient = client;
            HttpClient.Client.BaseAddress = new Uri(BaseUrl);
        }

        public void SetUnauthenticated()
            => HttpClient.Authenticated = false;

        public void UnauthorizeUser()
        {
            SetUnauthenticated();
            HttpClient.UserRoles.Clear();
            HttpClient.Client.DefaultRequestHeaders.Authorization = null;
        }

        private CargoRequestHandler cargoRequestHandler;
        public CargoRequestHandler CargoRequestHandler => cargoRequestHandler ??= cargoRequestHandler = new CargoRequestHandler(HttpClient);

        private AuthenticationRequestHandler authenticationRequestHandler;
        public AuthenticationRequestHandler AuthenticationRequestHandler => authenticationRequestHandler ??= authenticationRequestHandler = new AuthenticationRequestHandler(HttpClient);

        public CargoCategoriesRequestHandler cargoCategoriesRequestHandler;
        public CargoCategoriesRequestHandler CargoCategoriesRequestHandler => cargoCategoriesRequestHandler ??= cargoCategoriesRequestHandler = new CargoCategoriesRequestHandler(HttpClient);

        public CustomerRequestHandler customerRequestHandler;
        public CustomerRequestHandler CustomerRequestHandler => customerRequestHandler ??= customerRequestHandler = new CustomerRequestHandler(HttpClient);

        public OrderRequestHandler orderRequestHandler;
        public OrderRequestHandler OrderRequestHandler => orderRequestHandler ??= orderRequestHandler = new OrderRequestHandler(HttpClient);

        public RouteRequestHandler routeRequestHandler;
        public RouteRequestHandler RouteRequestHandler => routeRequestHandler ??= routeRequestHandler = new RouteRequestHandler(HttpClient);

        public TransportRequestHandler transportRequestHandler;
        public TransportRequestHandler TransportRequestHandler => transportRequestHandler ??= transportRequestHandler = new TransportRequestHandler(HttpClient);
    }
}
