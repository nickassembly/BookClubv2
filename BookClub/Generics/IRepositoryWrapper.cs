using BookClub.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Generics
{

    // TODO: Refactor Repo Wrapper/ Repo
    public interface IRepositoryWrapper
    {
        IAuthorRepository AuthorRepo { get; }
        IAuthorUserRepository AuthorUserRepo { get; }
        IAuthorBookRepository AuthorBookRepo { get; }
        IAuthorGenreRepository AuthorGenreRepo { get; }

        IBookRepository UserBookRepo { get; }
        void Save();
    }
}
