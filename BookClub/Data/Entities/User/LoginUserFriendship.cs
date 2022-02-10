using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Data.Entities.User
{
    public class LoginUserFriendship
    {
        public string UserId { get; set; }
        public LoginUser User { get; set; }
        public string UserFriendId { get; set; }
        public LoginUser UserFriend { get; set; }
    }
}
