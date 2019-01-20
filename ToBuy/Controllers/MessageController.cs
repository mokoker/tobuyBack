using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TB.Db;
using TB.Db.Services;
using ToBuy.Common.DTOs;
using ToBuy.Middleware;

namespace ToBuy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController :  BaseBController
    {
        private MessageService service;
        public MessageController()
        {
            ToBuyContext context = new ToBuyContext();
            service = new MessageService(context);
        }
        [JwtAuth(Common.Enums.Roles.User)]
        [HttpPost]
        public void Post([FromBody] MessageDto value)
        {
            value.SenId = UserId;
            service.AddNewMessage(value);
        }


        [HttpPost("PostDev")]
        public void PostDev([FromBody] MessageDto value)
        {
            service.AddNewMessage(value);
        }

        [JwtAuth(Common.Enums.Roles.User)]
        [HttpGet]
        public List<MessageDto> GetAllMessages()
        {
            return service.GetUserMessages(UserId);

        }

    }
}