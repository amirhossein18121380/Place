using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Place.API.CustomAttribute
{
    public class ApiAuthorizeAttribute : ActionFilterAttribute
    {
        public ApiAuthorizeAttribute()
        {
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            //string secret = context.HttpContext.Request.Headers["secret"];
            //if (string.IsNullOrEmpty(secret))
            //    throw new MissingSecretException();
            await next();
        }
    }
}
