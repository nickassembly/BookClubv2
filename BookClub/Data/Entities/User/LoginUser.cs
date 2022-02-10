using BookClub.Data.Entities.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public class LoginUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public ICollection<LoginUserFriendship> FriendsOf { get; set; }
        public ICollection<LoginUserFriendship> Friends { get; set; }
        public ICollection<UserBook> UserBooks { get; set; }
        public ICollection<UserAuthor> UserAuthors { get; set; }
    }
}
