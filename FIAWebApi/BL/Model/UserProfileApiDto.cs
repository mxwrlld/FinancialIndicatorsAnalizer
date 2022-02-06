using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FIADbContext.Model.DTO;

namespace FIAWebApi.BL.Model
{
    public class UserProfileApiDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string Enterprise { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public UserProfileApiDto() { }

        public UserProfileApiDto(UserDbDTO user)
        {
            UserName = user.UserName;
            Email = user.Email;
            FirstName = user.FirstName;
            MiddleName = user.MiddleName;
            LastName = user.LastName;
            Enterprise = user.EntepriseTIN;
        }

        public UserProfileApiDto(UserDbDTO user, IEnumerable<string> roles) : this(user)
        {
            Roles = roles;
        }

        public void Update(UserDbDTO user)
        {
            user.Email = Email;
            user.FirstName = FirstName;
            user.MiddleName = MiddleName;
            user.LastName = LastName;
            user.EntepriseTIN = Enterprise;
        }

        public UserDbDTO Create()
        {
            return new UserDbDTO()
            {
                UserName = UserName,
                Email = Email,
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                EntepriseTIN = Enterprise
            };
        }
    }

    public class UserProfileCreateApiDto : UserProfileApiDto
    {
        public string Password { get; set; }
    }
}
