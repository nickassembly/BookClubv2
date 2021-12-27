using BookClub.Data;
using BookClub.Data.Entities;

namespace BookClub.Generics
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private BookClubContext _bookclubContext;
        private IAuthorRepository _authorRepo;
        private IAuthorUserRepository _authorUserRepo;
        private IAuthorGenreRepository _authorGenreRepo;
        private IBookRepository _bookRepo;
        private IBookUserRepository _bookUserRepo;
        private IBookAuthorRepository _bookAuthorRepo;
        private IBookGenreRepository _bookGenreRepo;

        public RepositoryWrapper(BookClubContext bookclubContext)
        {
            _bookclubContext = bookclubContext;
        }
        public IBookRepository BookRepo
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

        public IBookUserRepository UserBookRepo
        {
            get
            {
                if (_bookUserRepo == null)
                {
                    _bookUserRepo = new BookUserRepository(_bookclubContext);
                }

                return _bookUserRepo;
            }
        }

        public IBookAuthorRepository BookAuthorRepo
        {
            get
            {
                if (_bookAuthorRepo == null)
                {
                    _bookAuthorRepo = new BookAuthorRepository(_bookclubContext);
                }

                return _bookAuthorRepo;
            }
        }

        public IBookGenreRepository BookGenreRepo
        {
            get
            {
                if (_bookGenreRepo == null)
                {
                    _bookGenreRepo = new BookGenreRepository(_bookclubContext);
                }

                return _bookGenreRepo;
            }
        }

        public IAuthorRepository AuthorRepo
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

        public IAuthorUserRepository UserAuthorRepo
        {
            get
            {
                if (_authorUserRepo == null)
                {
                    _authorUserRepo = new AuthorUserRepository(_bookclubContext);
                }

                return _authorUserRepo;
            }
        }

        public IAuthorGenreRepository AuthorGenreRepo
        {
            get
            {
                if (_authorGenreRepo == null)
                {
                    _authorGenreRepo = new AuthorGenreRepository(_bookclubContext);
                }

                return _authorGenreRepo;
            }
        }

        public void Save()
        {
            _bookclubContext.SaveChanges();
        }
    }
}
