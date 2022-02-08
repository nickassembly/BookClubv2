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

   // TODO: Best way to implement follow function to get notified for new other user's interests (Authors, books etc.)
   // Research Api and possibly change schema to track following
}
