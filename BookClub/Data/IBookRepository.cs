﻿using BookClub.Data.Entities;
using System.Collections.Generic;

namespace BookClub.Data
{
    public interface IBookRepository
    {
        IEnumerable<UserBook> GetAllUserBooks();
        IEnumerable<Book> GetBooksByAuthor(Author author);
        bool SaveAll();
    }
}