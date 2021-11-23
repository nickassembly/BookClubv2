using BookClub.Data.Entities;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookClub.Controllers
{
    [Route("api/[controller]/[action]")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorController : Controller
    {

        // TODO: Figure out models
        // Repo layer (tutorial)
        // Pull in Db Context

        [HttpGet]
        public async Task<IActionResult> UserAuthorList()
        {
            var authors = new List<AuthorViewModel>();

     
            return View(authors);
        }

    }
}
