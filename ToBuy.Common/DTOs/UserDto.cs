using System;
using System.Collections.Generic;
using System.Text;
using ToBuy.Common.Enums;

namespace ToBuy.Common.DTOs
{
    public class UserDto : BaseDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles UserRoles { get; set; }
        public string Token { get; set; }
    }
}
