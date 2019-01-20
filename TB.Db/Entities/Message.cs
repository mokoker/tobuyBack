using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ToBuy.Common.DTOs;

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

        public override MessageDto GetDto(MessageDto dto)
        {
            dto.Receiver = Receiver.UserName;
            dto.Sender = Sender.UserName;
            dto.SentDate = SentDate;
            dto.Text = Text;
            return dto;
        }

        public override void MapEntity(MessageDto dto)
        {
            this.Text = dto.Text;
            this.ReceiverId = dto.RecId;
            this.SenderId = dto.SenId;
        }
    }
}
