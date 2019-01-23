using System;
using System.Collections.Generic;
using System.Text;

namespace ToBuy.Common.Enums
{
    [Flags]
    public enum Roles
    {
        None = 0,
        Administrator = 1<<0,
        User = 1<<1,
        Admin = Administrator |User
    }
}
