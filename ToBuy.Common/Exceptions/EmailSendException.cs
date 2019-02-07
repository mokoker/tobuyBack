using System;
using System.Collections.Generic;
using System.Text;

namespace ToBuy.Common.Exceptions
{
    public class EmailSendException :Exception
    {
        public EmailSendException(string message):base(message)
        {
            
        }
    }
}
