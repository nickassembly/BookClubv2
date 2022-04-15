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
using System.Security.Claims;

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

            // TODO: Abstract these methods out, find more efficent method (utility method?)
            // to map between Books - BookVM - BookUser - ETC

            List<LoginUserFriendship> userFriendIds = _context.LoginUserFriendships.Where(u => u.UserId == userId).ToList();
           
            List<FriendBookListVM> friendBookLists = new();

            foreach (var friendId in userFriendIds)
            {
                var friendBookIds = FriendBookIds(friendId.UserFriendId);
                var friendName = _userManager.Users.FirstOrDefault(u => u.Id == friendId.UserFriendId);

                var friendBooks = _context.Books.Where(b => friendBookIds.Contains(b.Id));

                List<BookViewModel> friendBookVMs = new();
                foreach (var book in friendBooks)
                {
                    BookViewModel friendBookVM = _mapper.Map<BookViewModel>(book);
                    friendBookVMs.Add(friendBookVM);
                }

                FriendBookListVM friendBookList = new FriendBookListVM
                {
                    FriendBooks = friendBookVMs,
                    FriendName = friendName.UserName
                };

                friendBookLists.Add(friendBookList);
            }

            List<BookViewModel> userBookList = new();
            foreach (var loggedInUserBook in userBooks)
            {
                BookViewModel userBookVM = _mapper.Map<BookViewModel>(loggedInUserBook);
                userBookList.Add(userBookVM);
            }

            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
           
            LoginUserProfileViewModel loginProfile = new LoginUserProfileViewModel
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                UserBookList = userBookList,
                FriendBookList = friendBookLists,
                Friends = userFriendIds
            };

            return View(loginProfile);
        }

        public List<int> FriendBookIds(string userFriendId)
        {
            return _context.UserBooks.Where(user => user.UserId == userFriendId).Select(x => x.BookId).ToList();
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
