using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.Data.Entities.User;
using BookClub.Utils;
using BookClub.ViewModels;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!this.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var userId = UserUtils.GetLoggedInUser(this.User);

            // TODO: Extract to Method, use Automapper to create VMS, return VMS to this method to hydrate model
            var userBookIds = _context.UserBooks.Where(x => x.UserId == userId).Select(x => x.BookId);

            var userBooks = _context.Books.Where(b => userBookIds.Contains(b.Id));

            List<BookViewModel> userBookList = new();

            foreach (var book in userBooks)
            {
                BookViewModel bookVM = new BookViewModel
                {

                };

                userBookList.Add(bookVM);
            }

            LoginUserProfileViewModel loginProfile = new LoginUserProfileViewModel
            {
                UserBookList = userBookList,
               // Friends = userFriendsList
            };

            return View(loginProfile);
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
