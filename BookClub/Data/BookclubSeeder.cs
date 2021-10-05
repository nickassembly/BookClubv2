using BookClub.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Data
{
    public class BookclubSeeder
    {
        private readonly BookClubContext _ctx;
        public BookclubSeeder(BookClubContext ctx)
        {
            _ctx = ctx;
        }
        public void Seed()
        {
            _ctx.Database.EnsureCreated();

            if (!_ctx.Books.Any())
            {
                // Need to create sample data
                var book = new Book()
                {
                    Category = "Fantasy",
                    Price = 19.99,
                    Title = "The Trouble with Peace",                    
                    Description = "Joe Abercrombie kicking ass",
                    Identifier = "120998234",
                    IdentifierType = "ISBN",
                    Authors = new Author() { Firstname = "Joe", Lastname = "Abercrombie" }
                };
                _ctx.Books.Add(book);

                _ctx.SaveChanges();
            }
        }
    }
}
