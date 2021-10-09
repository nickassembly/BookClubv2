using BookClub.Data.Entities;
using System.Collections.Generic;

namespace BookClub.Data
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