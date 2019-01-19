using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TB.Db;
using TB.Db.Services;
using ToBuy.Common.DTOs;
using ToBuy.Common.Exceptions;

namespace ToBuy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseBController
    {
        [HttpPost]
        public IActionResult Post([FromBody] UserDto user)
        {
            user.UserRoles = Common.Enums.Roles.User;
            ToBuyContext context = new ToBuyContext();
            UserService service = new UserService(context);
            try
            {
             var x=  service.AddNewUser(user);
                return Ok(x);
            }
            catch (UserExistsException)
            {
                return Conflict("user already exists");
            }
     
        }

        [HttpPost("Login")]
        public IActionResult Login(UserDto user)
        { 
            HttpResponseMessage response;
            ToBuyContext context = new ToBuyContext();
            UserService service = new UserService(context);
            UserDto result = service.Login(user);

            if (result == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}

