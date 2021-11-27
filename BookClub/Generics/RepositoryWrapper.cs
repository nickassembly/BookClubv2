using BookClub.Data;
using BookClub.Data.Entities;

namespace BookClub.Generics
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private BookClubContext _bookclubContext;
        private IAuthorRepository _authorRepo;

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
       

        public void Save()
        {
            _bookclubContext.SaveChanges();
        }
    }
}
