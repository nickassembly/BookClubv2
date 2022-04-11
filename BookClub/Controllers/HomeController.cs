using AutoMapper;
using BookClub.Core.IConfiguration;
using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.Data.Entities.User;
using BookClub.Utils;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BookClub.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookClubContext _context;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IMapper mapper, BookClubContext context, UserManager<LoginUser> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {

            if (!this.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var userId = UserUtils.GetLoggedInUser(User);

            var userBookIds = _context.UserBooks.Where(x => x.UserId == userId).Select(x => x.BookId);

            var userBooks = _context.Books.Where(b => userBookIds.Contains(b.Id));

            List<BookViewModel> userBookList = new();

            List<LoginUserFriendship> userFriendIds = _context.LoginUserFriendships.Where(u => u.UserId == userId).ToList();
           
            List<FriendBookListVM> friendBookLists = new();

            foreach (var friendId in userFriendIds)
            {
                var friendBooks = GetFriendBooks(friendId.UserFriendId);
                var friendName = _userManager.Users.FirstOrDefault(u => u.Id == friendId.UserFriendId);

                FriendBookListVM friendBookList = new FriendBookListVM
                {
                    FriendBooks = friendBooks,
                    FriendName = friendName.UserName
                };

                friendBookLists.Add(friendBookList);
            }

            LoginUserProfileViewModel loginProfile = new LoginUserProfileViewModel
            {
                UserBookList = userBookList,
                FriendBookList = friendBookLists,
                Friends = userFriendIds
            };

            return View(loginProfile);
        }

        public List<UserBook> GetFriendBooks(string userFriendId)
        {
            return _context.UserBooks.Where(user => user.UserId == userFriendId).ToList();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
