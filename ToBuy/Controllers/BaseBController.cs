using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        protected string IpAddress
        {
            get
            {
                return accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            }
        }
    }
}
