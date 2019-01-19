using System;
using System.Collections.Generic;
using System.Text;

namespace TB.Db.Services
{
    public class BaseService
    {
        protected ToBuyContext context;
        public BaseService(ToBuyContext context)
        {
            this.context = context;

        }
    }
}
