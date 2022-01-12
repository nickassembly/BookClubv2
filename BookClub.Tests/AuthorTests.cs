using BookClub.Controllers;
using BookClub.Core.IConfiguration;
using BookClub.Core.Repositories;
using BookClub.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BookClub.Tests
{
    public class AuthorTests
    {

        [Fact]
        public async Task List_Authors()
        {
           
            List<Author> testAuthors = new List<Author>()
            {
                new Author() { Id = 1, Firstname = "Bob", Lastname = "Smith"},
                new Author() { Id = 2, Firstname = "Tom", Lastname= "Ace"}
            };

            List<UserAuthor> testUserAuthors = new List<UserAuthor>()
            {
                new UserAuthor() { Id = 1, UserId = "TestUserId", AuthorId = 1, Author = new Author { Id = 1 }, User = new LoginUser { Id = "TestUserId" }},
                new UserAuthor() { Id = 2, UserId = "TestUserId", AuthorId = 2, Author = new Author { Id = 2 }, User = new LoginUser { Id = "TestUserId" }}
            };

            List<AuthorBook> testAuthorBooks = new List<AuthorBook>()
            {
                new AuthorBook() { Id = 1, AuthorId = 1, BookId = 1 },
                new AuthorBook() { Id = 2, AuthorId = 1, BookId = 2},
                new AuthorBook() { Id = 3, AuthorId = 2, BookId = 1}
            };

            List<AuthorGenre> testAuthorGenres = new List<AuthorGenre>()
            {
                new AuthorGenre() { Id = 1, AuthorId = 1, GenreId = 1 },
                new AuthorGenre() { Id = 2, AuthorId = 1, GenreId = 2 }
            };

            List<Book> testBooks = new List<Book>()
            {
                new Book() { Id = 1, Title = "Test book 1" },
                new Book() { Id = 2, Title = "Test book 2"}
            };

            List<Genre> testGenres = new List<Genre>()
            {
                new Genre() { Id = 1, GenreName = "Genre 1" },
                new Genre() { Id = 2, GenreName = "Genre 2" }
            };

            var mockAuthorRepo = new Mock<GenericRepository<Author>>();
            var mockUserAuthorRepo = new Mock<GenericRepository<UserAuthor>>();
            var mockAuthorBookRepo = new Mock<GenericRepository<AuthorBook>>();
            var mockBookRepo = new Mock<GenericRepository<Book>>();
            var mockAuthorGenreRepo = new Mock<GenericRepository<AuthorGenre>>();
            var mockGenre = new Mock<GenericRepository<Genre>>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockAuthorRepo.Setup(repo => repo.All()).Returns(It.IsAny<Task<IEnumerable<Author>>>);
            mockUserAuthorRepo.Setup(uaRepo => uaRepo.All()).Returns(It.IsAny<Task<IEnumerable<UserAuthor>>>);
            mockAuthorBookRepo.Setup(abRepo => abRepo.All()).Returns(It.IsAny<Task<IEnumerable<AuthorBook>>>);
            mockBookRepo.Setup(abRepo => abRepo.All()).Returns(It.IsAny<Task<IEnumerable<Book>>>);
            mockAuthorGenreRepo.Setup(abRepo => abRepo.All()).Returns(It.IsAny<Task<IEnumerable<AuthorGenre>>>);
            mockGenre.Setup(abRepo => abRepo.All()).Returns(It.IsAny<Task<IEnumerable<Genre>>>);

            mockUnitOfWork.Setup(ub => ub.AuthorBooks.All()).Returns(Task.FromResult<IEnumerable<AuthorBook>>(testAuthorBooks));
            mockUnitOfWork.Setup(uow => uow.Authors.All()).Returns(Task.FromResult<IEnumerable<Author>>(testAuthors));
            mockUnitOfWork.Setup(ua => ua.AuthorUsers.All()).Returns(Task.FromResult<IEnumerable<UserAuthor>>(testUserAuthors));
            mockUnitOfWork.Setup(b => b.Books.All()).Returns(Task.FromResult<IEnumerable<Book>>(testBooks));
            mockUnitOfWork.Setup(ag => ag.AuthorGenres.All()).Returns(Task.FromResult<IEnumerable<AuthorGenre>>(testAuthorGenres));
            mockUnitOfWork.Setup(g => g.Genres.All()).Returns(Task.FromResult<IEnumerable<Genre>>(testGenres));

            var controller = new AuthorController(mockUnitOfWork.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    { 
                    new Claim(ClaimTypes.NameIdentifier, "TestUserId")
                    }, "someAuthTypeName"))
                }
            };         

            var result = await controller.UserAuthorList();

            // TODO: Create Asserts for List Test

        }
    }
}


