using BookClub.Data;
using BookClub.Data.Entities;

namespace BookClub.Generics
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private BookClubContext _bookclubContext;
        private IAuthorRepository _authorRepo;
        private IBookRepository _bookRepo;

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
        public IBookRepository UserBookRepo
        {
            get
            {
                if (_bookRepo == null)
                {
                    _bookRepo = new BookRepository(_bookclubContext);
                }

                return _bookRepo;
            }
        }

        public void Save()
        {
            _bookclubContext.SaveChanges();
        }
    }
}
