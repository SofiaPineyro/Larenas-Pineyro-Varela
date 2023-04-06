using ArenaGestor.BusinessInterface;
using ArenaGestor.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ArenaGestor.API.Filters
{
    public class AuthorizationFilter : Attribute, IAuthorizationFilter
    {

        private readonly RoleCode[] roles;

        private ISecurityService _securityService;
        public AuthorizationFilter(params RoleCode[] roles)
        {
            this.roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _securityService = context.HttpContext.RequestServices.GetService<ISecurityService>();
            string token = context.HttpContext.Request.Headers["token"];

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Content = "You aren't logued."
                };
                return;
            }
            try
            {
                if (_securityService.GetUserOfToken(token) == null)
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = StatusCodes.Status403Forbidden,
                        Content = "The token is invalid"
                    };
                    return;
                }
            }
            catch (Exception)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Content = "The token is invalid"
                };
                return;
            }
            bool isAuth = false;
            foreach (var item in roles)
            {
                if (_securityService.UserHaveRole(item, token))
                {
                    isAuth = true;
                }
            }

            if (!isAuth)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Content = "You don't have permissions to do this action"
                };
            }

        }
    }
}
