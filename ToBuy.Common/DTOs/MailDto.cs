using System;
using System.Collections.Generic;
using System.Text;

namespace ToBuy.Common.DTOs
{
   public class MailDto:BaseDto
    {
        public string SenderAddress { get; set; }
        public string HtmlMessage { get; set; }
        public string Textmessage { get; set; }
        public string Subject { get; set; }
        public string ReceiverAddress { get; set; }
    }
}
