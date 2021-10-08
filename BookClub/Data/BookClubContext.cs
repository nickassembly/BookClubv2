﻿using BookClub.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Data
{
    public class BookClubContext : IdentityDbContext<LoginUser>
    {
        private readonly DbContextOptions<BookClubContext> _options;
        private readonly IConfiguration _config;
        private readonly UserManager<LoginUser> _userManager;

        public BookClubContext(DbContextOptions<BookClubContext> options, IConfiguration config, UserManager<LoginUser> userManager) : base(options)
        {
            _options = options;
            _config = config;
            _userManager = userManager;
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<LoginUser> LoginUsers { get; set; }
        
    }
}
