using BookClub.Data.Entities;
using BookClub.Generics;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookClub.Controllers
{
    [Route("api/[controller]/[action]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorController : Controller
    {
        private readonly ILogger<AuthorController> _logger;
        private IRepositoryWrapper _repoWrapper;
        private readonly UserManager<LoginUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorController(ILogger<AuthorController> logger,
            IRepositoryWrapper repoWrapper,
            UserManager<LoginUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        public async Task<IActionResult> UserAuthorList()
        {
            ClaimsPrincipal currentUser = this.User;

            if (currentUser.Claims.Any())
            {
                var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

                var userAuthors = _repoWrapper.UserAuthorRepo.ListByCondition(x => x.UserId == currentUserId).ToList();

                // Need to get Authors from List of User Authors, need to map to author object
                // This may require adding an additional instance of the IRepositoryWrapper?
                // need to research best way to handle this.


                return View(userAuthors.ToList());
            }

            return RedirectToAction("Login", "Account");

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
           // var authors = _repoWrapper.UserAuthorRepo.List();

            // TODO:  need a wrapper around regular Author object, just User Author...

            return View();
        }

    }
}
