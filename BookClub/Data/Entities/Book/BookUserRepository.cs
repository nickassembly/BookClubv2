using BookClub.Generics;

namespace BookClub.Data.Entities
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(BookClubContext bookclubContext) : base(bookclubContext)
        {

        }
    }

    public class BookUserRepository : RepositoryBase<UserBook>, IBookUserRepository
    {
        public BookUserRepository(BookClubContext bookclubContext) : base(bookclubContext)
        {

        }
    }

    public class BookAuthorRepository : RepositoryBase<BookAuthor>, IBookAuthorRepository
    {
        public BookAuthorRepository(BookClubContext bookclubContext) : base(bookclubContext)
        {

        }
    }

    public class BookGenreRepository : RepositoryBase<BookGenre>, IBookGenreRepository
    {
        public BookGenreRepository(BookClubContext bookclubContext) : base(bookclubContext)
        {

        }
    }
}
