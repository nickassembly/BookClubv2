using BookClub.Data;
using BookClub.Utils;
using Google.Apis.Books.v1.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookClub.Data.Entities;
using BookClub.ViewModels;

namespace BookClub.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _repository;
        private readonly ILogger<BookController> _logger;
        private readonly string googleAPIKey;
        private readonly IMapper _mapper;

        public BookController(IBookRepository repository, ILogger<BookController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            googleAPIKey = "AIzaSyCjqD7OtvMLj-JMh3erdPRh_qWyRJvnvxw";
            _mapper = mapper;
        }

        [Authorize]
        public IActionResult UserBookList()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var results = _repository.GetAllUserBooks();

                    return View(results);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error getting book list: {ex}");
                    return View();
                }
            }
            else return View();
        }

        public IActionResult GetBookDetails(BookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BookSearch bookSearch = new();
                    var isbn = bookViewModel.Title.FirstOrDefault().ToString();
                    var results = bookSearch.SearchISBN(isbn);

                    return View(results);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error getting book list: {ex}");
                    return View();
                }
            }
            else return View();
        }

        public IActionResult AddNewBookForUser([FromForm] BookViewModel newBookModel)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("/Views/Shared/_AddUserBookPartial.cshtml", newBookModel);
            }

            var newBook = _mapper.Map<Book>(newBookModel);
            var successful = _repository.AddNewUserbook(newBook);
            if (!successful)
            {
                return BadRequest("Could not add item.");
            }
            var results = _repository.GetAllUserBooks();
            return View("UserBookList", results);
        }

        [HttpGet]
        [Route("api/book")]
        public IActionResult BookList()
        {
            try
            {
                var results = _repository.GetAllBooks();
                return Ok(results);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
