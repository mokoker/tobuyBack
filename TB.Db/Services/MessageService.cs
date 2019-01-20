using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TB.Db.Entities;
using ToBuy.Common.DTOs;

namespace TB.Db.Services
{
    public class MessageService : BaseService
    {
        public MessageService(ToBuyContext context) : base(context)
        {
           

        }
        public void AddNewMessage(MessageDto dto)
        {
            Message msg = Message.GetEnt(dto);
            msg.SentDate = DateTime.Now;
            context.Messages.Add(msg);
            context.SaveChanges();
        }
        public List<MessageDto> GetUserMessages(int UserId)
        {
            List<MessageDto> messages = new List<MessageDto>();
            var resultEnt = context.Messages.Where(x => x.ReceiverId == UserId).Include(z => z.Sender).Include(j=>j.Receiver);
            foreach (Message m in resultEnt)
            {
                messages.Add(m.GetDto());
            }
            return messages;
        }
    }
}
