using BookClub.Data.Entities;
using BookClub.Generics;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Controllers
{
    [Route("api/[controller]/[action]")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorController : Controller
    {
        private readonly ILogger<AuthorController> _logger;
        private IRepositoryWrapper _repoWrapper;

        public AuthorController(ILogger<AuthorController> logger, IRepositoryWrapper repoWrapper)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
        }

        // TODO: Change to only get User Authors
        [HttpGet]
        public async Task<IActionResult> UserAuthorList()
        {
            var authors = _repoWrapper.AuthorRepo.List();

            return View(authors.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = _repoWrapper.AuthorRepo.List();

            return View(authors.ToList());
        }

    }
}
