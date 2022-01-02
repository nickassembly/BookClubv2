﻿using BookClub.Core.Repositories;
using System.Threading.Tasks;

namespace BookClub.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; }
        IAuthorUserRepository AuthorUsers { get; }
        IAuthorBookRepository AuthorBooks { get; }
        IAuthorGenreRepository AuthorGenres { get; }
        IBookRepository Books { get; }
        IGenreRepository Genres { get; }

        Task CompleteAsync();
    }
}
