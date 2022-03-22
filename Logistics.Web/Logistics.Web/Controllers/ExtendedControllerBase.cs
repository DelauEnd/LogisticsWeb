using Logistics.Models.WebModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RequestHandler;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CargoTransportation.Controllers
{
    public class ExtendedControllerBase : Controller
    {
        private IRequestManager _request;
        protected IRequestManager request => _request ??= HttpContext.RequestServices.GetService<IRequestManager>();

        protected static StringContent BuildHttpContent(object user)
            => new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        protected ActionResult UnsuccesfullStatusCode(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                request.SetUnauthenticated();

            return new ObjectResult(new StatusCodeResponse()
            {
                StatusCode = ((int)response.StatusCode).ToString(),
                Response = response.ReasonPhrase
            });
        }
    }
}
