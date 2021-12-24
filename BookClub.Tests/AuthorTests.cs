using BookClub.Data.Entities;
using BookClub.Generics;
using BookClub.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace BookClub.Tests
{
    public class AuthorTests
    {
     
        [Fact]
        public void ReturnsListOfAuthors()
        {
            // TODO: Get Logged in User through method
            string userId = "7c173ffb-d6b4-4ace-aca7-db8cdcfcaf16";

            // Function should return this list, use as mock
            List<AuthorViewModel> authorViewModels = new List<AuthorViewModel>
            {
                new AuthorViewModel() { },
                new AuthorViewModel() { },
                new AuthorViewModel() { },
                new AuthorViewModel() { },
                new AuthorViewModel() { },
            };

            var respository = new Mock<RepositoryWrapper>();

            respository.Setup(x => x.AuthorUserRepo.ListByCondition(y => y.UserId == userId)).Returns((List<int> authorIds) => )    
            
        }
    }
}
