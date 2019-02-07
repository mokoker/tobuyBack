using System;
using Microsoft.EntityFrameworkCore;
using TB.Db;
using TB.Db.Services;
using ToBuy.Common.DTOs;
using ToBuy.Common.Enums;

namespace ToBuy.Helpers
{
    public class EmailCreator
    {
        private MailService service;
        public EmailCreator(ToBuyContext context)
        {
            service = new MailService(context);
        }
        public MailDto GeneratePasswordMail()
        {
           MailDto dto = service.GetMailTemplate(MailType.PasswordReminder);
            return dto;
        }

        public MailDto GenerateMessageMail(string userName,string messageFrom,string mailTo,string content)
        {
            MailDto dto = service.GetMailTemplate(MailType.GotMessage);
            dto.HtmlMessage = dto.HtmlMessage.Replace("{{UserName}}", userName)
                .Replace("{{MessageFrom}}", messageFrom)
                .Replace("{{Date}}", DateTime.Now.ToShortTimeString())
                .Replace("{{Message}}",content);
            dto.Textmessage = dto.Textmessage.Replace("{{UserName}}", userName)
                .Replace("{{MessageFrom}}", messageFrom)
                .Replace("{{Message}}", content);
            dto.ReceiverAddress = mailTo;
            return dto;
        }
    }
}
