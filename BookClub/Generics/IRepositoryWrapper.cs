using BookClub.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Generics
{
    public interface IRepositoryWrapper
    {
        IAuthorRepository AuthorRepo { get; }
        IAuthorUserRepository UserAuthorRepo { get; }
        IAuthorGenreRepository AuthorGenreRepo { get; }
        IBookRepository BookRepo { get; }
        IBookUserRepository UserBookRepo { get; }
        IBookAuthorRepository BookAuthorRepo { get; }
        IBookGenreRepository BookGenreRepo { get; }

        void Save();
    }
}
