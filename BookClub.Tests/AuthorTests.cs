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
                new Author() { Id = 1, Firstname = "Bob",   Lastname = "Smith"},
                new Author() { Id = 2, Firstname = "Tom", Lastname= "Ace"}
            };

            List<UserAuthor> testUserAuthors = new List<UserAuthor>()
            {
                new UserAuthor() { },
                new UserAuthor() { }
            };

            // TODO: Add User authors

            var mockRepo = new Mock<GenericRepository<Author>>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockRepo.Setup(repo => repo.All()).Returns(It.IsAny<Task<IEnumerable<Author>>>);

            mockUnitOfWork.Setup(uow => uow.Authors.All()).Returns(Task.FromResult<IEnumerable<Author>>(testAuthors));
            



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


        }
    }
}


