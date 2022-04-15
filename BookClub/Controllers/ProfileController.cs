using BookClub.Core.IConfiguration;
using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.Data.Entities.User;
using BookClub.Utils;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace BookClub.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly BookClubContext _context;

        public ProfileController(ILogger<ProfileController> logger, IUnitOfWork unitOfWork, BookClubContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(LoginUserProfileViewModel loggedInUser)
        {
            if (!this.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            string currentUserId = UserUtils.GetLoggedInUser(User);

            var loggedInUserFriendIds = _context.LoginUserFriendships.Where(x => x.UserId == currentUserId).Select(x => x.UserFriendId).ToList();

            List<LoginUserFriendship> userFriends = new();

            foreach (var friendId in loggedInUserFriendIds)
            {
                var user = _context.Users.Where(x => x.Id == friendId).FirstOrDefault();

                LoginUserFriendship userFriend = new LoginUserFriendship
                {
                    User = user,
                    UserId = friendId
                };
               
                userFriends.Add(userFriend);
            }

            loggedInUser.Friends = userFriends;
            

            return View(loggedInUser);
        }

    }
}
