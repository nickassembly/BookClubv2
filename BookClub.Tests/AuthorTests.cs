using BookClub.Controllers;
using BookClub.Core.IConfiguration;
using BookClub.Core.Repositories;
using BookClub.Data.Entities;
using Moq;
using Xunit;

namespace BookClub.Tests
{
    public class AuthorTests
    {

        [Fact]
        public void ReturnsListOfAuthors()
        {
            //bool searchWasCalled = false; // ??

            //Mock<IGenericRepository<Author>> repositoryMock = new Mock<IGenericRepository<Author>>();
            //repositoryMock.Setup(author => author.All()).Callback(() => searchWasCalled = true);

            //var mockUnitOfWork = new Mock<IUnitOfWork>();
            //mockUnitOfWork.Setup(uow => uow.Authors).Returns(repositoryMock.Object);

 
        }

        [Fact]
        public void AddsNewAuthorToUserAuthors()
        {

        }
    }
}
