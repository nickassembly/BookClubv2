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
        IAuthorBookRepository AuthorBookRepo { get; }
        IAuthorGenreRepository AuthorGenreRepo { get; }

        void Save();
    }
}
