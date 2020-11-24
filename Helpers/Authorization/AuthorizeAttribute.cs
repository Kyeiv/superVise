using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using superVise.Entities;

namespace superVise.Helpers.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (IsAnonymousEndpoint(context)) 
                return;

            UnauthorizeIfUserNotLogged(context);
        }

        private void UnauthorizeIfUserNotLogged(AuthorizationFilterContext context)
        {
            var user = (User) context.HttpContext.Items["User"];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new {message = "Unauthorized"})
                    {StatusCode = StatusCodes.Status401Unauthorized};
            }
        }

        private bool IsAnonymousEndpoint(AuthorizationFilterContext context)
        {
            return context.ActionDescriptor.EndpointMetadata
                .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));
        }
    }
}