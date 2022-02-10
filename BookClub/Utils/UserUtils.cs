using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookClub.Utils
{
    public static class UserUtils
    {
        public static string GetLoggedInUser(ClaimsPrincipal currentUser)
        {

            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            return currentUserId;
        }
    }

}
