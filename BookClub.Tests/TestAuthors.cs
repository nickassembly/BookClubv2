using BookClub.Controllers;
using BookClub.Core.IConfiguration;
using BookClub.Core.Repositories;
using BookClub.Data.Entities;
using BookClub.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace BookClub.Tests
{
    public class TestAuthors
    {

        [Fact]
        public async Task AuthorList_ShouldReturn_Authors()
        {
           
            List<Author> testAuthors = new List<Author>()
            {
                new Author() { Id = 1, Firstname = "Bob", Lastname = "Smith"},
                new Author() { Id = 2, Firstname = "Tom", Lastname= "Ace"}
            };

            List<UserAuthor> testUserAuthors = new List<UserAuthor>()
            {
                new UserAuthor() { Id = 1, UserId = "TestUserId", AuthorId = 1, Author = testAuthors[0], User = new LoginUser { Id = "TestUserId" }},
                new UserAuthor() { Id = 2, UserId = "TestUserId", AuthorId = 2, Author = testAuthors[1], User = new LoginUser { Id = "TestUserId" }}
            };

            List<AuthorBook> testAuthorBooks = new List<AuthorBook>()
            {
                new AuthorBook() { Id = 1, AuthorId = 1, BookId = 1},
                new AuthorBook() { Id = 2, AuthorId = 1, BookId = 2},
                new AuthorBook() { Id = 3, AuthorId = 2, BookId = 1}
            };

            List<AuthorGenre> testAuthorGenres = new List<AuthorGenre>()
            {
                new AuthorGenre() { Id = 1, AuthorId = 1, GenreId = 1 },
                new AuthorGenre() { Id = 2, AuthorId = 1, GenreId = 2 },
                new AuthorGenre() { Id = 3, AuthorId = 2, GenreId = 1 }
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

            Assert.NotNull(result);
            Assert.Contains(testUserAuthors, item => item.Author.Firstname == "Tom" && item.Author.Lastname == "Ace");
            Assert.Contains(testUserAuthors, item => item.Author.Firstname == "Bob" && item.Author.Lastname == "Smith");
        }

        [Fact]
        public void AddAuthor_ShouldCreateNewAuthor()
        {
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

            AuthorViewModel testAddAuthorVM = new AuthorViewModel
            {
                
                Firstname = "Keller",
                Lastname = "Car",
                Nationality = "Hahnville",
                BiographyNotes = "Keller was a Car all his life",
                BookIds = new List<int> { 1, 2 },
                GenreIds = new List<int> { 1, 2}
            };

            Author testAddAuthor = new Author
            {
                //Id = 1,
                Firstname = testAddAuthorVM.Firstname,
                Lastname = testAddAuthorVM.Lastname,
                Nationality = testAddAuthorVM.Nationality,
                BiographyNotes = testAddAuthorVM.BiographyNotes
            };

            var mockAuthorRepo = new Mock<GenericRepository<Author>>();
            var mockGenre = new Mock<GenericRepository<Genre>>();
            var mockBookRepo = new Mock<GenericRepository<Book>>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockAuthorRepo.Setup(repo => repo.Add(testAddAuthor)).Returns(Task.FromResult(true));

            mockBookRepo.Setup(abRepo => abRepo.All()).Returns(It.IsAny<Task<IEnumerable<Book>>>);
            mockGenre.Setup(abRepo => abRepo.All()).Returns(It.IsAny<Task<IEnumerable<Genre>>>);

            mockUnitOfWork.Setup(uow => uow.Authors.Add(testAddAuthor)).Returns(Task.FromResult(true));

            mockUnitOfWork.Setup(b => b.Books.All()).Returns(Task.FromResult<IEnumerable<Book>>(testBooks));
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

            var result = controller.AddAuthor(testAddAuthorVM);
            // Authors.Add Unit of work not assigning Id during test



        }

    }
}


