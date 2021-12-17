using BookClub.Generics;

namespace BookClub.Data.Entities
{
    public interface IAuthorRepository : IRepositoryBase<UserAuthor>
    {
        // TODO: Add properties for BookAuthors and GenreAuthors into this wrapper
        // TODO: Create IBookRepository Wrapper : IRepositoryBase<UserBook>?
    }
}
