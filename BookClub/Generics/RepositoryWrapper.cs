using BookClub.Data;
using BookClub.Data.Entities;

namespace BookClub.Generics
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private BookClubContext _bookclubContext;
        private IAuthorRepository _authorRepo;
        private IAuthorBookRepository _authorBookRepo;

        public RepositoryWrapper(BookClubContext bookclubContext)
        {
            _bookclubContext = bookclubContext;
        }

        public IAuthorRepository UserAuthorRepo
        {
            get
            {
                if (_authorRepo == null)
                {
                    _authorRepo = new AuthorRepository(_bookclubContext);
                }

                return _authorRepo;
            }
        }

        public IAuthorBookRepository AuthorBookRepo
        {
            get
            {
                if (_authorBookRepo == null)
                {
                    _authorBookRepo = new AuthorBookRepository(_bookclubContext);
                }

                return _authorBookRepo;
            }
        }

        public void Save()
        {
            _bookclubContext.SaveChanges();
        }
    }
}
