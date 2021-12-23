using BookClub.Data;
using BookClub.Data.Entities;

namespace BookClub.Generics
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private BookClubContext _bookclubContext;
        private IAuthorRepository _authorRepo;
        private IAuthorUserRepository _authorUserRepo;
        private IAuthorBookRepository _authorBookRepo;
        private IAuthorGenreRepository _authorGenreRepo;
        private IBookRepository _bookRepo;

        public RepositoryWrapper(BookClubContext bookclubContext)
        {
            _bookclubContext = bookclubContext;
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

        public IAuthorUserRepository AuthorUserRepo
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

        public IAuthorGenreRepository AuthorGenreRepo
        {
            get
            {
                if (_authorGenreRepo == null)
                {
                    _authorGenreRepo = new AuthorGenreRepository(_bookclubContext);
                }

                return _authorGenreRepo;
                
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
