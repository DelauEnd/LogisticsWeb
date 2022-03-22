using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RequestHandler;

namespace CargoTransportation.ActionFilters
{
    public class AuthenticatedAttribute : IActionFilter
    {
        private IRequestManager RequestManager { get; set; }

        public AuthenticatedAttribute(IRequestManager request)
        {
            this.RequestManager = request;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!RequestManager.HttpClient.Authenticated)
                context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}
