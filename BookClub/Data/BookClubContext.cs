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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(b => b.Book)
                .WithMany(ba => ba.BookAuthors)
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(a => a.Author)
                .WithMany(ba => ba.AuthorBooks)
                .HasForeignKey(ai => ai.AuthorId);

            modelBuilder.Entity<BookGenre>()
                .HasOne(b => b.Book)
                .WithMany(gb => gb.GenreBooks)
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<AuthorGenre>()
                .HasOne(a => a.Author)
                .WithMany(ga => ga.AuthorGenres)
                .HasForeignKey(ai => ai.AuthorId);

            modelBuilder.Entity<UserBook>()
                .HasOne(u => u.User)
                .WithMany(ub => ub.UserBooks)
                .HasForeignKey(ui => ui.UserId);

            modelBuilder.Entity<UserBook>()
                .HasOne(b => b.Book)
                .WithMany(ub => ub.BookUsers)
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<UserAuthor>()
                .HasOne(u => u.User)
                .WithMany(ua => ua.UserAuthors)
                .HasForeignKey(ui => ui.UserId);

            modelBuilder.Entity<UserAuthor>()
                .HasOne(a => a.Author)
                .WithMany(ua => ua.UserAuthors)
                .HasForeignKey(ai => ai.AuthorId);

            modelBuilder.Entity<Book>()
                .HasOne(p => p.Publisher)
                .WithMany(b => b.Books)
                .HasForeignKey(pi => pi.PublisherId);

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<LoginUser> LoginUsers { get; set; }
        public DbSet<UserAuthor> UserAuthors { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> GenreBooks { get; set; }
        public DbSet<AuthorGenre> GenreAuthors { get; set; }


    }
}
