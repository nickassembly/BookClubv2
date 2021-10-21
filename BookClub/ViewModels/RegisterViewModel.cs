using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public class RegisterUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string emailAddress { get; set; }
        public string Password { get; set; }
        public string VerifyPassword { get; set; }

    }
}
