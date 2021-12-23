using BookClub.Generics;

namespace BookClub.Data.Entities
{
    public interface IAuthorRepository : IRepositoryBase<Author>
    {

    }
    public interface IAuthorUserRepository : IRepositoryBase<UserAuthor>
    {

    }

    public interface IAuthorBookRepository : IRepositoryBase<AuthorBook>
    {

    }

    public interface IAuthorGenreRepository : IRepositoryBase<AuthorGenre>
    {

    }

}
