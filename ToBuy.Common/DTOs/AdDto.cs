using System;
using System.Collections.Generic;
using System.Text;
using ToBuy.Common.Enums;

namespace ToBuy.Common.DTOs
{
    public class AdDto : BaseDto
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string Message { get; set; }
        public int PosterId { get; set; }
        public string PosterName { get; set; }
        public string CategoryName { get; set; }
        public bool ToSell { get; set; }
        public DateTime PostDate { get; set; }

        public Cities City { get; set; }
        public string CityName
        {
            get
            {
                return Enum.GetName(typeof(Cities), City);
            }
        }


    }
}
