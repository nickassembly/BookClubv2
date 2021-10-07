using BookClub.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _repository;

        public BookController(IBookRepository repository)
        {
            _repository = repository;
        }
        [Authorize]
        public IActionResult BookList()
        {
            //TODO: User and Bookclub context to retrieve books for  logged in user
            var results = _repository.GetAllUserBooks();
            return View(results);
        }
    }
}
