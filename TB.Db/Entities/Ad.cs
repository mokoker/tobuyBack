using Newtonsoft.Json;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ToBuy.Common.DTOs;
using ToBuy.Common.Enums;

namespace TB.Db.Entities
{
    public class Ad : BaseEntity<Ad, AdDto>
    {
        public string Title { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("PosterId")]
        public User Poster { get; set; }
        public int PosterId { get; set; }
        public string Message { get; set; }
        public Cities City { get; set; }
        public DateTime PostDate { get; set; }
        public bool ToSell { get; set; }
        public NpgsqlTsVector SearchVector { get; set; }
        public float Price { get; set; }
        public PostState State { get; set; }
        public string IpAddress { get; set; }

        public override AdDto GetDto(AdDto dto)
        {
            dto.Title = Title;
            dto.CategoryId = CategoryId;
            if (Poster != null)
            {
                dto.PosterName = Poster.UserName;
            }
            if (Category != null)
            {
                dto.CategoryName = Category.Name;
            }
            dto.PosterId = PosterId;
            dto.Id = Id;
            dto.Message = Message;
            dto.PostDate = PostDate;
            dto.ToSell = ToSell;
            dto.Price = Price;
            dto.City = City;
            return dto;
        }

        public override void MapEntity(AdDto dto)
        {
            Title = dto.Title;
            City = dto.City;
            Message = dto.Message;
            CategoryId = dto.CategoryId;
            PosterId = dto.PosterId;
            PostDate = dto.PostDate;
            ToSell = dto.ToSell;
            Price = dto.Price;
            IpAddress = dto.IpAddress;

        }
    }
}
