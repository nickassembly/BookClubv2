using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookClub.Authors
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _repository;
        public AuthorController(IAuthorRepository repository)
        {
            _repository = repository;
        }

        [Authorize]
        public IActionResult AuthorList()
        {
            var results = _repository.GetAllUserAuthors();
            return View(results);
        }
    }
}
