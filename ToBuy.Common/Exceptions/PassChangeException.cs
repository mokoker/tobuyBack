using System;
using System.Collections.Generic;
using System.Text;

namespace ToBuy.Common.Exceptions
{
    public class PassChangeException :Exception
    {
        public PassChangeException(string message):base(message)
        {

        }
    }
}
