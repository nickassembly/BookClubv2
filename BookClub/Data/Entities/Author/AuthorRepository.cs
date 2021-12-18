﻿using BookClub.Generics;

namespace BookClub.Data.Entities
{
    public class AuthorRepository : RepositoryBase<UserAuthor>, IAuthorRepository
    {
        public AuthorRepository(BookClubContext bookclubContext) : base(bookclubContext)
        {

        }
    }

    public class AuthorBookRepository : RepositoryBase<AuthorBook>, IAuthorBookRepository
    {
        public AuthorBookRepository(BookClubContext bookclubContext) : base(bookclubContext)
        {

        }
    }
}
