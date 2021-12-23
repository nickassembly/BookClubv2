using BookClub.Generics;

namespace BookClub.Data.Entities
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(BookClubContext bookclubContext) : base(bookclubContext)
        {

        }
    }

    public class AuthorUserRepository : RepositoryBase<UserAuthor>, IAuthorUserRepository
    {
        public AuthorUserRepository(BookClubContext bookclubContext) : base(bookclubContext)
        {

        }
    }

    public class AuthorBookRepository : RepositoryBase<AuthorBook>, IAuthorBookRepository
    {
        public AuthorBookRepository(BookClubContext bookclubContext) : base(bookclubContext)
        {

        }
    }

    public class AuthorGenreRepository : RepositoryBase<AuthorGenre>, IAuthorGenreRepository
    {
        public AuthorGenreRepository(BookClubContext bookclubContext) : base(bookclubContext)
        {

        }
    }
}
