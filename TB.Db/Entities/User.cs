using System;
using System.Collections.Generic;
using System.Text;
using ToBuy.Common.DTOs;
using ToBuy.Common.Enums;

namespace TB.Db.Entities
{
    public class User : BaseEntity<User, UserDto>
    {
        private const string salt = "$2a$10$llw0G6IyibUoba3eXRt9xuRczaGdCm/AiV6SSjf5v78XS824EGbh.";
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PassHash { get; set; }
        public Roles UserRoles { get; set; }
        public List<Ad> Ads { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string MailSecret { get; set; }
        public DateTime SecretDate { get; set; }
        public string IpAddress { get; set; }

        public override UserDto GetDto(UserDto dto)
        {
            dto.UserName = UserName;
            dto.UserRoles = UserRoles;
            dto.Email = Email;
            dto.Id = Id;
            return dto;
        }

        public override void MapEntity(UserDto dto)
        {
            UserName = dto.UserName;
            Email = dto.Email;
            SettPass(dto.Password);
            Id = dto.Id;
            UserRoles = dto.UserRoles;
            RegistrationDate = DateTime.Now;
            IpAddress = dto.IpAddress;
        }

        public void SettPass(string pass)
        {
            PassHash = BCrypt.Net.BCrypt.HashPassword(pass, salt);
        }
    }
}
