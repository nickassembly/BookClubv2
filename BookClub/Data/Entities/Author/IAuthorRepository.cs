using BookClub.Generics;

namespace BookClub.Data.Entities
{
    public interface IAuthorRepository : IRepositoryBase<UserAuthor>
    {
        // TODO: Create IBookRepository Wrapper : IRepositoryBase<UserBook>?
    }

    public interface IAuthorBookRepository : IRepositoryBase<AuthorBook>
    {

    }

}
