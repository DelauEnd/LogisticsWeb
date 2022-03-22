using RequestHandler.ModelHandlers;

namespace RequestHandler
{
    public interface IRequestManager
    {
        public IHttpClientService HttpClient { get; }
        public AuthenticationRequestHandler AuthenticationRequestHandler { get; }
        public CargoCategoriesRequestHandler CargoCategoriesRequestHandler { get; }
        public CargoRequestHandler CargoRequestHandler { get; }
        public CustomerRequestHandler CustomerRequestHandler { get; }
        public OrderRequestHandler OrderRequestHandler { get; }
        public RouteRequestHandler RouteRequestHandler { get; }
        public TransportRequestHandler TransportRequestHandler { get; }

        public void SetUnauthenticated();
        public void UnauthorizeUser();
    }
}
