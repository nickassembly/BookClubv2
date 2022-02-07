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
        public async Task AddAuthor_ShouldCreateNewAuthor()
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
                Id = 1,
                Firstname = "Keller",
                Lastname = "Car",
                Nationality = "Hahnville",
                BiographyNotes = "Keller was a Car all his life",
                BookIds = new List<int> { 1, 2 },
                GenreIds = new List<int> { 1, 2 }
            };

            Author testAddAuthor = new Author
            {
                Id = testAddAuthorVM.Id,
                Firstname = testAddAuthorVM.Firstname,
                Lastname = testAddAuthorVM.Lastname,
                Nationality = testAddAuthorVM.Nationality,
                BiographyNotes = testAddAuthorVM.BiographyNotes
            };

            List<UserAuthor> testUserAuthors = new List<UserAuthor>
            {
                new UserAuthor { Id = 1, UserId = "TestUserId", AuthorId = 1, Author = testAddAuthor, User = new LoginUser { Id = "TestUserId" }}
            };

            List<AuthorBook> testAuthorBooks = new List<AuthorBook>()
            {
                new AuthorBook() { Id = 1, AuthorId = testAddAuthor.Id, BookId = testBooks[0].Id},
                new AuthorBook() { Id = 2, AuthorId = testAddAuthor.Id, BookId = testBooks[1].Id}
            };

            List<AuthorGenre> testAuthorGenres = new List<AuthorGenre>()
            {
                new AuthorGenre() { Id = 1, AuthorId = testAddAuthor.Id, GenreId = testGenres[0].Id },
                new AuthorGenre() { Id = 2, AuthorId = testAddAuthor.Id, GenreId = testGenres[1].Id }
            };

            var mockAuthorRepo = new Mock<GenericRepository<Author>>();
            var mockUserAuthorRepo = new Mock<GenericRepository<UserAuthor>>();
            var mockGenre = new Mock<GenericRepository<Genre>>();
            var mockBookRepo = new Mock<GenericRepository<Book>>();
            var mockAuthorBook = new Mock<GenericRepository<AuthorBook>>();
            var mockAuthorGenre = new Mock<GenericRepository<AuthorGenre>>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockAuthorRepo.Setup(repo => repo.Add(testAddAuthor)).Returns(Task.FromResult(true));
            mockUserAuthorRepo.Setup(uaRepo => uaRepo.All()).Returns(It.IsAny<Task<IEnumerable<UserAuthor>>>);

            mockBookRepo.Setup(abRepo => abRepo.All()).Returns(It.IsAny<Task<IEnumerable<Book>>>);
            mockGenre.Setup(abRepo => abRepo.All()).Returns(It.IsAny<Task<IEnumerable<Genre>>>);

            mockAuthorGenre.Setup(agenrerepo => agenrerepo.All()).Returns(It.IsAny<Task<IEnumerable<AuthorGenre>>>);
            mockAuthorBook.Setup(abookrepo => abookrepo.All()).Returns(It.IsAny<Task<IEnumerable<AuthorBook>>>);

            mockUnitOfWork.Setup(uow => uow.Authors.Add(testAddAuthor)).Returns(Task.FromResult(true));
            mockUnitOfWork.Setup(ua => ua.AuthorUsers.All()).Returns(Task.FromResult<IEnumerable<UserAuthor>>(testUserAuthors));
            mockUnitOfWork.Setup(b => b.Books.All()).Returns(Task.FromResult<IEnumerable<Book>>(testBooks));
            mockUnitOfWork.Setup(g => g.Genres.All()).Returns(Task.FromResult<IEnumerable<Genre>>(testGenres));
            mockUnitOfWork.Setup(ag => ag.AuthorGenres.All()).Returns(Task.FromResult<IEnumerable<AuthorGenre>>(testAuthorGenres));
            mockUnitOfWork.Setup(ab => ab.AuthorBooks.All()).Returns(Task.FromResult<IEnumerable<AuthorBook>>(testAuthorBooks));


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

            var result = await controller.AddAuthor(testAddAuthorVM);

            Assert.NotNull(result);
            Assert.Contains(testUserAuthors, item => item.Author.Firstname == "Keller" && item.Author.Lastname == "Car");

        }

        [Fact]
        public async Task GetAuthorById_ShouldReturnAuthor()
        {
            Author testAuthor = new Author
            {
                Id = 1,
                Firstname = "Bob",
                Lastname = "Smith"
            };

            UserAuthor testUserAuthor = new UserAuthor
            {
                Id = 1,
                UserId = "TestUserId",
                AuthorId = 1,
                Author = testAuthor,
                User = new LoginUser { Id = "TestUserId" }
            };

            List<AuthorBook> testAuthorBooks = new List<AuthorBook>()
            {
                new AuthorBook() { Id = 1, AuthorId = 1, BookId = 1},
                new AuthorBook() { Id = 2, AuthorId = 1, BookId = 2},
            };

            List<AuthorGenre> testAuthorGenres = new List<AuthorGenre>()
            {
                new AuthorGenre() { Id = 1, AuthorId = 1, GenreId = 1 },
                new AuthorGenre() { Id = 2, AuthorId = 1, GenreId = 2 },
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
            mockUserAuthorRepo.Setup(uaRepo => uaRepo.GetById(testUserAuthor.Id)).Returns(It.IsAny<Task<UserAuthor>>);
           
            mockAuthorBookRepo.Setup(abRepo => abRepo.All()).Returns(It.IsAny<Task<IEnumerable<AuthorBook>>>);
            mockBookRepo.Setup(abRepo => abRepo.All()).Returns(It.IsAny<Task<IEnumerable<Book>>>);
            mockAuthorGenreRepo.Setup(abRepo => abRepo.All()).Returns(It.IsAny<Task<IEnumerable<AuthorGenre>>>);
            mockGenre.Setup(abRepo => abRepo.All()).Returns(It.IsAny<Task<IEnumerable<Genre>>>);

            mockUnitOfWork.Setup(ub => ub.AuthorBooks.All()).Returns(Task.FromResult<IEnumerable<AuthorBook>>(testAuthorBooks));

            mockUnitOfWork.Setup(uow => uow.Authors.All()).Returns(It.IsAny<Task<IEnumerable<Author>>>);
            mockUnitOfWork.Setup(ua => ua.AuthorUsers.GetById(testUserAuthor.Id)).Returns(Task.FromResult(testUserAuthor));

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

            var result = await controller.GetUserAuthorById(1);

            Assert.NotNull(result);
            Assert.Equal("Bob", testUserAuthor.Author.Firstname);
            Assert.Equal("Smith", testUserAuthor.Author.Lastname);
        }

        [Fact]
        public async Task DeleteUserAuthor_ShouldRemoveUserAuthor()
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
                Id = 1,
                Firstname = "Keller",
                Lastname = "Car",
                Nationality = "Hahnville",
                BiographyNotes = "Keller was a Car all his life",
                BookIds = new List<int> { 1, 2 },
                GenreIds = new List<int> { 1, 2 }
            };

            Author testAddAuthor = new Author
            {
                Id = testAddAuthorVM.Id,
                Firstname = testAddAuthorVM.Firstname,
                Lastname = testAddAuthorVM.Lastname,
                Nationality = testAddAuthorVM.Nationality,
                BiographyNotes = testAddAuthorVM.BiographyNotes
            };

            List<UserAuthor> testUserAuthors = new List<UserAuthor>
            {
                new UserAuthor { Id = 1, UserId = "TestUserId", AuthorId = 1, Author = testAddAuthor, User = new LoginUser { Id = "TestUserId" }}
            };

            List<AuthorBook> testAuthorBooks = new List<AuthorBook>()
            {
                new AuthorBook() { Id = 1, AuthorId = testAddAuthor.Id, BookId = testBooks[0].Id},
                new AuthorBook() { Id = 2, AuthorId = testAddAuthor.Id, BookId = testBooks[1].Id}
            };

            List<AuthorGenre> testAuthorGenres = new List<AuthorGenre>()
            {
                new AuthorGenre() { Id = 1, AuthorId = testAddAuthor.Id, GenreId = testGenres[0].Id },
                new AuthorGenre() { Id = 2, AuthorId = testAddAuthor.Id, GenreId = testGenres[1].Id }
            };

            var mockAuthorRepo = new Mock<GenericRepository<Author>>();
            var mockUserAuthorRepo = new Mock<GenericRepository<UserAuthor>>();
            var mockGenre = new Mock<GenericRepository<Genre>>();
            var mockBookRepo = new Mock<GenericRepository<Book>>();
            var mockAuthorBook = new Mock<GenericRepository<AuthorBook>>();
            var mockAuthorGenre = new Mock<GenericRepository<AuthorGenre>>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockAuthorRepo.Setup(repo => repo.Add(testAddAuthor)).Returns(Task.FromResult(true));
            mockUserAuthorRepo.Setup(uaRepo => uaRepo.All()).Returns(It.IsAny<Task<IEnumerable<UserAuthor>>>);

            mockBookRepo.Setup(abRepo => abRepo.All()).Returns(It.IsAny<Task<IEnumerable<Book>>>);
            mockGenre.Setup(abRepo => abRepo.All()).Returns(It.IsAny<Task<IEnumerable<Genre>>>);

            mockAuthorGenre.Setup(agenrerepo => agenrerepo.All()).Returns(It.IsAny<Task<IEnumerable<AuthorGenre>>>);
            mockAuthorBook.Setup(abookrepo => abookrepo.All()).Returns(It.IsAny<Task<IEnumerable<AuthorBook>>>);

            mockUnitOfWork.Setup(uow => uow.Authors.Add(testAddAuthor)).Returns(Task.FromResult(true));
            mockUnitOfWork.Setup(ua => ua.AuthorUsers.All()).Returns(Task.FromResult<IEnumerable<UserAuthor>>(testUserAuthors));
            mockUnitOfWork.Setup(b => b.Books.All()).Returns(Task.FromResult<IEnumerable<Book>>(testBooks));
            mockUnitOfWork.Setup(g => g.Genres.All()).Returns(Task.FromResult<IEnumerable<Genre>>(testGenres));
            mockUnitOfWork.Setup(ag => ag.AuthorGenres.All()).Returns(Task.FromResult<IEnumerable<AuthorGenre>>(testAuthorGenres));
            mockUnitOfWork.Setup(ab => ab.AuthorBooks.All()).Returns(Task.FromResult<IEnumerable<AuthorBook>>(testAuthorBooks));


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

            var result = await controller.AddAuthor(testAddAuthorVM);
            // Add then delete?


        }

    }
}


