using BookClub.Generics;

namespace BookClub.Data.Entities
{
    public interface IBookRepository : IRepositoryBase<Book>
    {

    }
    public interface IBookUserRepository : IRepositoryBase<UserBook>
    {

    }

    public interface IBookAuthorRepository : IRepositoryBase<BookAuthor>
    {

    }

    public interface IBookGenreRepository : IRepositoryBase<BookGenre>
    {

    }

}
