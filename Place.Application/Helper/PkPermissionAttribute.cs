using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Place.Application.Helper
{
    public class PkPermissionAttribute : TypeFilterAttribute
    {
        public PkPermissionAttribute() : base(typeof(PkPermissionAuthorize))
        {
        }
    }

    public class PkPermissionAuthorize : IAuthorizationFilter
    {
        //private readonly ResourcesEnum _resources;


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var roles = context.HttpContext.User.FindAll(c => c.Type == ClaimTypes.Role);
            //if (roles == null || roles.All(c => c.Value != _resources.ToString()))
            //{
            //    context.Result = new ForbidResult();
            //}
        }
    }
}
