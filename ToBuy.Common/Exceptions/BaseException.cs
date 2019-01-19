using System;
using System.Collections.Generic;
using System.Text;

namespace ToBuy.Common.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException(string message):base(message)
        {
            
        }
    }
}
