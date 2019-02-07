using System;
using System.Collections.Generic;
using System.Text;
using TB.Db.Entities;
using ToBuy.Common.DTOs;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Linq;
using ToBuy.Common.Exceptions;

namespace TB.Db.Services
{
    public class UserService : BaseService
    {
    
        public UserService(ToBuyContext context) : base(context)
        {
          
        }

        public UserDto AddNewUser(UserDto dto)
        {
            var existing = context.Users.FirstOrDefault(x => x.Email == dto.Email);
            if (existing != null)
            {
                throw new UserExistsException("User exists");
            }
            User user = User.GetEnt(dto);
            context.Users.Add(user);
            context.SaveChanges();
            dto.Token = TokenHelper.GenerateToken(user);
            dto.Password = "";
            return dto;
        }
        public UserDto GetUser(int id)
        {
            return context.Users.Single(x => x.Id == id).GetDto();
        }

        public UserDto Login(UserDto dto)
        {
            User userFromUser = User.GetEnt(dto);
            var user = context.Users.SingleOrDefault(x => x.Email == dto.Email && x.PassHash == userFromUser.PassHash);
            if (user != null)
            {
                var x = user.GetDto();
                x.Token = TokenHelper.GenerateToken(user);
                return x;
            }
            else
            {
                return null;
            }
        }


    

    }
}
