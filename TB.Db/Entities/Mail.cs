using System;
using System.Collections.Generic;
using System.Text;
using ToBuy.Common.DTOs;
using ToBuy.Common.Enums;

namespace TB.Db.Entities
{
    public class Mail : BaseEntity<Mail, MailDto>
    {   
        public string SenderAddress { get; set; }
        public string HtmlMessage { get; set; }
        public string Textmessage { get; set; }
        public string Subject { get; set; }
        public MailType MType { get; set; }

        public override MailDto GetDto(MailDto dto)
        {
            dto.HtmlMessage = HtmlMessage;
            dto.SenderAddress = SenderAddress;
            dto.Textmessage = Textmessage;
            dto.Subject = Subject;
            dto.Id = Id;
            return dto;
        }

        public override void MapEntity(MailDto dto)
        {
            Id = dto.Id;
            SenderAddress = dto.SenderAddress;
            Textmessage = dto.Textmessage;
            Subject = dto.Subject;
            HtmlMessage = dto.HtmlMessage;
        }
    }
}
