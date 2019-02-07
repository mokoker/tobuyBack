using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ToBuy.Controllers
{
    public class BaseBController : ControllerBase
    {
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
    }
}
