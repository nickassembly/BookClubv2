using BookClub.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Data
{
    public class BookclubSeeder
    {
        private readonly BookClubContext _ctx;
        private readonly IWebHostEnvironment _hosting;
        private readonly UserManager<LoginUser> _userManager;
        public BookclubSeeder(BookClubContext ctx, IWebHostEnvironment hosting, UserManager<LoginUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }
        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();
            LoginUser user = await _userManager.FindByEmailAsync("guerra.joseph@gmail.com");
            if (user == null)
            {
                user = new LoginUser()
                {
                    Firstname = "Joe",
                    Lastname = "Guerra",
                    Email = "guerra.joseph@gmail.com",
                    UserName = "guerra.joseph@gmail.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in Seeder");
                }
            }
            if (!_ctx.Books.Any())
            {
                var authors = new Author[]
               {
                    new Author {Firstname="joe", Lastname = "Guerra" },
                    new Author {Firstname="keller", Lastname = "car" },

               };
                // Need to create sample data
                var book = new Book()
                {
                    Category = "Fantasy",
                    Price = 19.99,
                    Title = "The Trouble with Peace",
                    Description = "Joe Abercrombie kicking ass",
                    Identifier = "120998234",
                    IdentifierType = "ISBN",
                  //  Authors = authors
                };
                _ctx.Books.Add(book);
            }
            if (!_ctx.UserBooks.Any())
            {
                var authors = new Author[]
                 {
                    new Author {Firstname="joe", Lastname = "Guerra" },
                    new Author {Firstname="keller", Lastname = "car" },

                };
                var books = new Book[]
                {
                    new Book {
                       // Authors = authors,
                        Category="New Category",
                        Title="New Title", 
                        Description = "New Description"
                    }
                };
                var userBook = new UserBook()
                {
                    User = user,
                  //  Books = books
                };
                _ctx.UserBooks.Add(userBook);
                _ctx.SaveChanges();
            }


        }
    }
}
