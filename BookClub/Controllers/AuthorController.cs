using BookClub.Data.Entities;
using BookClub.Generics;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookClub.Controllers
{
    [Route("api/[controller]/[action]")]
   // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorController : BaseApiController
    {
        // TODO: Figure out how Specification works or remove it.

        private readonly IRepository<Author> _repository;

        public AuthorController(
            IRepository<Author> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var results = new List<AuthorViewModel>();

            // TODO: Populate results

            return View(results);
        }



    }
}
