using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ToBuy.Common.DTOs;
using ToBuy.Common.Enums;

namespace TB.Db.Entities
{
    public class Message : BaseEntity<Message, MessageDto>
    {
        public string Text { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        [ForeignKey("SenderId")]
        public User Sender { get; set; }
        [ForeignKey("ReceiverId")]
        public User Receiver { get; set; }
        public DateTime SentDate { get; set; }
        public MessageStatus ReceiverStatus { get; set; }
        public MessageStatus SenderStatus { get; set; }
        public string IpAddress { get; set; }

        public override MessageDto GetDto(MessageDto dto)
        {
            dto.Id =Id;
            dto.Receiver = Receiver.UserName;
            dto.RecId = Receiver.Id;
            dto.Sender = Sender.UserName;
            dto.SenId = Sender.Id;
            dto.SentDate = SentDate;
            dto.Text = Text;
            return dto;
        }

        public override void MapEntity(MessageDto dto)
        {
            this.Id = dto.Id;
            this.Text = dto.Text;
            this.ReceiverId = dto.RecId;
            this.SenderId = dto.SenId;
            this.IpAddress = dto.IpAddress;
        }
    }
}
