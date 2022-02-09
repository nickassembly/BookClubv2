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

   // https://stackoverflow.com/questions/63253848/how-to-create-an-add-friend-functionality-between-two-individual-user-accounts-i
   // schema for many to many relationship
   // create friends list before follow function
}
