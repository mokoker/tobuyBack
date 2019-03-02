using System;
using System.Collections.Generic;
using System.Text;
using ToBuy.Common.Enums;

namespace ToBuy.Common.DTOs
{
    public class SearchAdDto
    {
        public string Filter { get; set; }
        public int Per_page { get; set; }
        public int Page { get; set; }
        public int CategoryId { get; set; }
        public bool ToSell { get; set; }
        public bool GetAll { get; set; }
        public int UserId { get; set; }
        public bool MyMessages { get; set; }
        public List<Cities> Cities { get; set; }



    }
}
