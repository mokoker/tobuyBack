using System.Collections.Generic;
using System.Linq;
using TB.Db.Entities;
using ToBuy.Common.DTOs;
using ToBuy.Common.Enums;

namespace TB.Db.Services
{
    public class MailService : BaseService
    {
        private static Dictionary<MailType, Mail> cache = new Dictionary<MailType, Mail>();
        public MailService(ToBuyContext context) : base(context)
        {
            
        }
        public MailDto GetMailTemplate(MailType type)
        {
            if(!cache.ContainsKey(type))
            {
                cache[type] = context.Mails.OrderByDescending(y => y.Id).First(x => x.MType == type);       
             }
            return cache[type].GetDto();
        }

    }
}
