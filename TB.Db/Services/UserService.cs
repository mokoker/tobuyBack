using System;
using System.Linq;
using TB.Db.Entities;
using ToBuy.Common.DTOs;
using ToBuy.Common.Exceptions;
using ToBuy.Common.Helpers;

namespace TB.Db.Services
{
    public class UserService : BaseService
    {

        public UserService(ToBuyContext context) : base(context)
        {

        }

        public UserDto AddNewUser(UserDto dto)
        {
            var existing = context.Users.FirstOrDefault(x => x.Email == dto.Email || x.UserName == dto.UserName);
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

        public ForgotPasswordResultDto ForgotPassword(string email)
        {
            var user = context.Users.SingleOrDefault(x => x.Email == email);
            if (user !=null && string.IsNullOrEmpty(user.MailSecret))
            {
                user.SecretDate = DateTime.Now;
                user.MailSecret = RandomString.GenerateCoupon(8);
                context.Update(user);
                context.SaveChanges();
                return new ForgotPasswordResultDto { Name = user.UserName, Secret = user.MailSecret };
            }
            else
            {
                return null;
            }
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

        public void ChangePass(ChangePassDto dto)
        {
            var secretUser = context.Users.SingleOrDefault(x => x.MailSecret == dto.Secret);
            if (secretUser == null)
            {
                throw new PassChangeException("No such thing");
            }
            secretUser.SettPass(dto.NewPassword);
            secretUser.MailSecret = null;
            context.SaveChanges();
        }
    }
}
