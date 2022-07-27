using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Security.Principal;
using Place.API.Exceptions;
using Place.Application.Identities.Interface;
using Place.Domain.Models;

namespace Place.API.CustomAttribute
{
    public enum Role
    {
        Null,
        SuperAdmin,
        Admin,
        SaleManager,
        Agent

    }
    public class VIIAuthorizeAttribute : ActionFilterAttribute
    {
        public List<Role> Roles { get; set; }

        public VIIAuthorizeAttribute(Role role)
        {
            this.Roles = new List<Role>() { role };
        }
        public VIIAuthorizeAttribute(params Role[] roles)
        {
            this.Roles = roles.ToList();
        }
        public VIIAuthorizeAttribute()
        {
            Roles = null;
        }

        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var jwtHelper = (IJWTHelper)context.HttpContext.RequestServices.GetService(typeof(IJWTHelper));
            string token = context.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
                throw new MissingTokenException();
            User user = null;
            var jwtSecurityKey =
                (((IConfiguration)context.HttpContext.RequestServices.
                    GetService(typeof(IConfiguration)))!).GetSection("JWTSecurityKey").Value;
            var isValidate = jwtHelper.ValidateToken(token, out user, jwtSecurityKey);
            if (!isValidate)
                throw new ForbidenAccessException();
            //if ((Roles != null && Roles.Any()) && !user.Roles.Any())
            //    throw new NotAuthorizedException();
            if (Roles == null || !Roles.Any())
            {
                var identities = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.MobilePhone,user.UserName),
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                });

                context.HttpContext.User = new GenericPrincipal(identities,null);
            }
            else
            {
                //var userRoles = user.Roles.Select(x => x.Value);
                //var hasRequiredRole = false;
                //var requiredRoles = Roles.Select(x => x.ToString());
                //foreach (var role in requiredRoles)
                //{
                //    hasRequiredRole = userRoles.Contains(role);
                //    if (hasRequiredRole)
                //        break;
                //}
                //if (!hasRequiredRole)
                //    throw new NotAuthorizedException();
                var appIdentity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.MobilePhone,user.UserName),
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                });
                //context.HttpContext.User = new GenericPrincipal(appIdentity, 
                //    user?.Roles?.Select(r => r.Value)?.ToArray());

                var li = new List<string>();
                context.HttpContext.User = new GenericPrincipal(appIdentity, null);
            }
            await next();
        }
    }
}
