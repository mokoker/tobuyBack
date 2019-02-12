using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TB.Db;
using TB.Db.Services;
using ToBuy.Common.DTOs;
using ToBuy.Common.Exceptions;
using ToBuy.Common.Helpers;
using ToBuy.Helpers;

namespace ToBuy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseBController
    {
        private ToBuyContext context;
        private UserService service;
        public UserController(IHttpContextAccessor accessor) :base(accessor)
        {
            context = new ToBuyContext();
            service = new UserService(context);
        }
        [HttpPost]
        public IActionResult Post([FromBody] UserDto user)
        {
            user.UserRoles = Common.Enums.Roles.User;
            user.IpAddress = IpAddress;
            try
            {
                var x = service.AddNewUser(user);
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

        [HttpPost("[Action]/{email}", Name = "ForgotPassword")]
        public void ForgotPassword(string email)
        {
            var secret = service.ForgotPassword(email);
            if (secret != null)
            {
                EmailCreator creator = new EmailCreator(context);
                var mail =  creator.GeneratePasswordMail(secret.Name, secret.Secret, email);
                EmailSender sender = new EmailSender();
                sender.SendMessage(mail);
            }
        }

        [HttpPatch]
        public IActionResult ForgotPassword([FromBody]ChangePassDto pass)
        {
            try
            {
                service.ChangePass(pass);
            }
            catch(PassChangeException exc)
            {
                return NotFound("no such thing");
            }
            return Ok();
        }
    }
}

