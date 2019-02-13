using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToBuy.Common.Enums;

namespace ToBuy.Controllers
{
    public class BaseBController : ControllerBase
    {
        protected IHttpContextAccessor accessor;
        public BaseBController(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }
        public BaseBController()
        {

        }
        protected int UserId
        {
            get
            {
                return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            }
        }
        protected string UserName
        {
            get
            {
                return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            }
        }

        protected Roles UserRole
        {
            get
            {
                return (Roles)Enum.Parse(typeof(Roles), User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);
            }
        }

        protected string IpAddress
        {
            get
            {
                return accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }
    }
}
