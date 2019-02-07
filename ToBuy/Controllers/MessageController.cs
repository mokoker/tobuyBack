using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TB.Db;
using TB.Db.Services;
using ToBuy.Common.DTOs;
using ToBuy.Common.Helpers;
using ToBuy.Helpers;
using ToBuy.Middleware;

namespace ToBuy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : BaseBController
    {
        private EmailCreator crea;
        private MessageService messageService;
        private UserService userService;
        public MessageController()
        {
            ToBuyContext context = new ToBuyContext();
            messageService = new MessageService(context);
            crea = new EmailCreator(context);
            userService = new UserService(context);
        }
        [JwtAuth(Common.Enums.Roles.User)]
        [HttpPost]
        public void Post([FromBody] MessageDto value)
        {
            value.SenId = UserId;
            messageService.AddNewMessage(value);
            var receiver = userService.GetUser(value.RecId);
            var mail = crea.GenerateMessageMail(receiver.UserName, UserName, receiver.Email, value.Text);
            EmailSender sender = new EmailSender();
            sender.SendMessage(mail);
        }
        [JwtAuth(Common.Enums.Roles.User)]
        [HttpDelete("{id}")]
        public void DeleteMessage(int id)
        {
            messageService.DeleteMessage(id, UserId);
        }

        [JwtAuth(Common.Enums.Roles.User)]
        [HttpGet]
        public List<MessageDto> GetAllMessages()
        {
            return messageService.GetUserMessages(UserId);

        }

    }
}