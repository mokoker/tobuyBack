using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TB.Db.Entities;
using ToBuy.Common.DTOs;
using ToBuy.Common.Enums;

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

        public void DeleteMessage(int messageId,int userId)
        {
            var resultEnt = context.Messages.Single(x => x.Id == messageId);
            if (resultEnt.ReceiverId == userId)
            {
                resultEnt.ReceiverStatus = MessageStatus.Deleted;
            }
            else if(resultEnt.SenderId == userId)
            {
                resultEnt.SenderStatus = MessageStatus.Deleted;
            }
            else
            {
                throw new ArgumentException("What kind of socery is this.");
            }
            context.SaveChanges();
        }
        public List<MessageDto> GetUserMessages(int UserId)
        {
            List<MessageDto> messages = new List<MessageDto>();
            var resultEnt = context.Messages.Where(x => x.ReceiverId == UserId && x.ReceiverStatus == MessageStatus.Unread || x.SenderId == UserId &&x.SenderStatus == MessageStatus.Unread).Include(z => z.Sender).Include(j=>j.Receiver).OrderByDescending(x=>x.SentDate);
            foreach (Message m in resultEnt)
            {
                messages.Add(m.GetDto());
            }
            return messages;
        }
    }
}
