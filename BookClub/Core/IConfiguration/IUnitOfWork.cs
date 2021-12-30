using BookClub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; }
        IAuthorUserRepository AuthorUsers { get; }
        IAuthorBookRepository AuthorBooks { get; }
        IAuthorGenreRepository AuthorGenres { get; }

        Task CompleteAsync();
    }
}
