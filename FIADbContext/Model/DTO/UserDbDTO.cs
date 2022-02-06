using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace FIADbContext.Model.DTO
{
    public class UserDbDTO : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string EntepriseTIN { get; set; }
        public EnterpriseDbDTO Enterprise { get; set; }
    }
}
