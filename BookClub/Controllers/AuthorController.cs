﻿using BookClub.Data;
using BookClub.Data.Entities;
using BookClub.Generics;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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

        private readonly BookClubContext _context;

        public AuthorController(ILogger<AuthorController> logger,
            IRepositoryWrapper repoWrapper,
            UserManager<LoginUser> userManager,
            IHttpContextAccessor httpContextAccessor, BookClubContext context)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> UserAuthorList()
        {
            if (!this.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            try
            {
                var currentUserId = GetLoggedInUser();

                List<AuthorViewModel> authorsToReturn = new List<AuthorViewModel>();

                var userAuthorIds = await _repoWrapper.UserAuthorRepo.ListByCondition(x => x.UserId == currentUserId).Select(y => y.AuthorId).ToListAsync();

                // TODO: Research better LINQ query to pull back child objects without so many DB trips
                foreach (var authorId in userAuthorIds)
                {
                    Author authorToAdd = await _context.Authors.Where(x => x.Id == authorId).FirstOrDefaultAsync();

                    // Get Bio from author bio table
                    AuthorBio authorBio = await _context.AuthorBios.Where(x => x.AuthorId == authorId).FirstOrDefaultAsync();

                    // Get a list of ints from Book Author Table that correspond to book ids that this author has written 
                    List<int> authorBooksIds = await _context.BookAuthors.Where(x => x.AuthorId == authorId).Select(y => y.BookId).ToListAsync();
                    
                    // Get a list of ints from Genre Author Table that correspond to genres ids that this author has written. 
                    List<int> authorGenreIds = await _context.GenreAuthors.Where(x => x.AuthorId == authorId).Select(y => y.GenreId).ToListAsync();

                    
                    // TODO: Need to get a list of actual book and genre objects based on list of Ids from Author Book and Author Genre table..
                    // ....or change back to a list of Book and Genre???
                    
                    List<AuthorBook> authorBooks = await _context.BookAuthors.Where(x => authorBooksIds.Contains(x.Id)).ToListAsync();



                    List<AuthorGenre> authorGenres = await _context.GenreAuthors.Where(x => authorGenreIds.Contains(x.Id)).ToListAsync();

                    AuthorViewModel authorVM = new AuthorViewModel
                    {
                        Firstname = authorToAdd.Firstname,
                        Lastname = authorToAdd.Lastname,
                        AuthorBio = authorBio,
                        Books = authorBooks,
                        Genres = authorGenres

                    };

                    authorsToReturn.Add(authorVM);
                }
                return View(authorsToReturn.ToList());

            }
            catch (Exception ex)
            {

                _logger.LogError($"List failed for Authors - Exception: {ex}");
                return StatusCode(500);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorViewModel authorVM)
        {
            if (!this.User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            Author author = new Author();

            try
            {
                var currentUserId = GetLoggedInUser();

                // TODO: Possibly simplify objects. AuthorGenre v ICollection<Genre> and books??
                AuthorBio authorBio = new AuthorBio();
                authorBio.Nationality = authorVM.AuthorBio.Nationality;
                authorBio.BiographyDescription = authorVM.AuthorBio.BiographyDescription;
                authorBio.DateOfBirth = authorVM.AuthorBio.DateOfBirth;
                _context.AuthorBios.Add(authorBio);

                List<AuthorGenre> authorGenres = new List<AuthorGenre>();
                authorGenres.AddRange(authorVM.Genres.ToList());
                _context.GenreAuthors.AddRange(authorGenres);

                List<AuthorBook> authorBooks = new List<AuthorBook>();
                authorBooks.AddRange(authorVM.Books.ToList());
                _context.BookAuthors.AddRange(authorBooks);

                author.Firstname = authorVM.Firstname;
                author.Lastname = authorVM.Lastname;
                author.AuthorBio = authorBio;
                author.AuthorGenres = authorGenres;
                author.AuthorBooks = authorBooks;

                var authorToAdd = await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();

                // Add newly created Author to the User's UserAuthor Table
 

            }
            catch (Exception ex)
            {
                _logger.LogError($"Add failed for Author: {author} - Exception: {ex}");
                return StatusCode(500);
            }


            return View();
        }

        private string GetLoggedInUser()
        {
            ClaimsPrincipal currentUser = this.User;

            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            return currentUserId;
        }

    }
}
