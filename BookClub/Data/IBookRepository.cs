using BookClub.Data.Entities;
using BookClub.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookClub.Data
{
    public interface IBookRepository
    {
        UserBook GetAllUserBooks();
        IEnumerable<Book> GetBooksByAuthor(Author author);
        IEnumerable<Book> GetAllBooks();
        bool SaveAll();
        void AddEntity(object model);
        Task<bool> AddItemAsync(GoogleBookVolume newItem);
        bool AddNewBook(Book newItem);
        bool AddNewUserbook(Book newBook);
    }
}