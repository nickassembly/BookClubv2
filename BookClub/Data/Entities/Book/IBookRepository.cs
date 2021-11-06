using BookClub.Data.Entities;
using System.Collections.Generic;

namespace BookClub.Data.Entities
{
    public interface IBookRepository
    {
        UserBook GetAllUserBooks();
        IEnumerable<Book> GetBooksByAuthor(Author author);
        IEnumerable<Book> GetAllBooks();
        bool SaveAll();
        void AddEntity(object model);
    }
}