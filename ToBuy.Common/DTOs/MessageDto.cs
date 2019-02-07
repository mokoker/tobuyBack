using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ToBuy.Common.DTOs
{
    public class MessageDto :BaseDto
    {
        public string Text { get; set; }
        public string Sender { get; set; }
        public int RecId { get; set; }
        public string Receiver { get; set; }  
        public int SenId { get; set; }
        public DateTime SentDate { get; set; }

    }
}
