using BookClub.Controllers;
using BookClub.Core.IConfiguration;
using BookClub.Core.Repositories;
using BookClub.Data.Entities;
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
                new Author() { Id = 1, Firstname = "Bob",   Lastname = "Smith"},
                new Author() { Id = 2, Firstname = "Tom", Lastname= "Ace"}
            };

            var mockRepo = new Mock<GenericRepository<Author>>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockRepo.Setup(repo => repo.All()).Returns(It.IsAny<Task<IEnumerable<Author>>>);

            // create user claim for test
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user1"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim("custom-claim", "example claim value"),
            }, "mock"));

            var controller = new AuthorController(mockUnitOfWork.Object); // created separate controller to test...may not be the best way
            controller.ControllerContext = new ControllerContext();

            // TODO: debug test to see what else we are missing, create asserts

            var result = await controller.UserAuthorList();


        }
    }
}


