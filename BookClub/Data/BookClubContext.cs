using BookClub.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookClub.Data
{
    public class BookClubContext : IdentityDbContext<LoginUser>
    {
        private readonly DbContextOptions<BookClubContext> _options;
        private readonly IConfiguration _config;        

        public BookClubContext(DbContextOptions<BookClubContext> options, IConfiguration config) : base(options)
        {
            _options = options;
            _config = config;            
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        public DbSet<UserAuthor> UserAuthors { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<LoginUser> LoginUsers { get; set; }
        
    }
}
