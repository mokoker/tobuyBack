using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TB.Db;
using ToBuy.Common.Enums;

namespace ToBuy.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class JwtAuthAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly Roles allowedRoles;
        public JwtAuthAttribute(Roles allowedRoles)
        {
            this.allowedRoles = allowedRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                var zzx = context.HttpContext.Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(zzx) && allowedRoles != Common.Enums.Roles.None)
                {
                    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
                    return;
                }
                try
                {
                    var yx = TokenHelper.Validate(zzx[0].Split("Bearer ")[1]);
                    var zz = (Roles)Enum.Parse(typeof(Roles), yx.FindFirst(x => x.Type == ClaimTypes.Role).Value);

                    if ((int)(zz & this.allowedRoles) != 0 || this.allowedRoles == Common.Enums.Roles.None)
                    {
                        user.AddIdentity(new ClaimsIdentity(yx.Identity));
                    }
                    else
                    {
                        throw new UnauthorizedAccessException();
                    }
                }
                catch
                {
                    if (allowedRoles != Common.Enums.Roles.None)
                    {
                        context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Unauthorized);
                        return;
                    }
           
                }
            }


        }
    }
}
