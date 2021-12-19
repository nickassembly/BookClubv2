using BookClub.Generics;

namespace BookClub.Data.Entities
{
    public interface IAuthorRepository : IRepositoryBase<UserAuthor>
    {

    }

    public interface IAuthorBookRepository : IRepositoryBase<AuthorBook>
    {

    }

    public interface IAuthorGenreRepository : IRepositoryBase<AuthorGenre>
    {

    }

}
