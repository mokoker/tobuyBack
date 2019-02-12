using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ToBuy.Common.DTOs
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string IpAddress{get;set;}
    }
}
