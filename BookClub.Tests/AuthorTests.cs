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
        private Mock<GenericRepository<Author>> _authorRepo;
        private Mock<IUnitOfWork> _mockUnitOfWork = new Mock<IUnitOfWork>();
        
        [Fact]
        public void List_Authors()
        {
            _authorRepo.Setup(a => a.All()).Returns(It.IsAny<Task<IEnumerable<Author>>>);
            _mockUnitOfWork.Setup(u => u.Authors.All()).Returns(It.IsAny<Task<IEnumerable<Author>>>);

            // https://stackoverflow.com/questions/37843641/unit-test-an-entity-framework-generic-repository-using-moq

        }
    }
}


