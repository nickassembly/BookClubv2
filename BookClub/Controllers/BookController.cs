using AutoMapper;
using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.Generics;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookClub.Controllers
{
    [Route("api/[controller]/[action]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private IRepositoryWrapper _repoWrapper;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly BookClubContext _context;

        public BookController(ILogger<BookController> logger,
            IRepositoryWrapper repoWrapper,
            UserManager<LoginUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            BookClubContext context, 
            IMapper mapper)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult UserBookList()
        {
            ClaimsPrincipal currentUser = this.User;

            if (!this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            List<UserBookViewModel> userBooks = new();

            var dbuserBooks = _repoWrapper.UserBookRepo.List();
            foreach (var book in dbuserBooks)
            {

                var userBookVM = _mapper.Map<UserBookViewModel>(book);

                userBooks.Add(userBookVM); ;
            }

            return View(userBooks);
        }

        [HttpPost]
        public IActionResult AddBook()
        {
            return View();
        }

    }
}
