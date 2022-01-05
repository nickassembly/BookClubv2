using BookClub.Controllers;
using BookClub.Core.IConfiguration;
using BookClub.Core.Repositories;
using BookClub.Data.Entities;
using Moq;
using System.Collections.Generic;
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
                new Author() { Id = 1, Firstname = "Bob",   Lastname = "Smith"},
                new Author() { Id = 2, Firstname = "Tom", Lastname= "Ace"}
            };

            var mockRepo = new Mock<GenericRepository<Author>>();
            
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockRepo.Setup(repo => repo.All()).Returns(It.IsAny<Task<IEnumerable<Author>>>);

            var controller = new AuthorController(mockUnitOfWork.Object); // created separate controller to test...may not be the best way

            var result = await controller.UserAuthorList();

            // Create Asserts -- check mock objects, Unit of Work
        }
    }
}


