using BookClub.Data.Entities;
using BookClub.Generics;
using BookClub.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public class BookRepository : RepositoryBase<UserBook>, IBookRepository
    {
        public BookRepository(BookClubContext bookclubContext) : base(bookclubContext)
        {

        }

    }
}
