using System;
using System.Collections.Generic;
using System.Text;

namespace ToBuy.Common.Exceptions
{
    public class UserException : BaseException
    {
        public UserException(string message) : base(message)
        {

        }
    }
    public class UserExistsException : BaseException
    {
        public UserExistsException(string message) : base(message)
        {

        }
    }
}
