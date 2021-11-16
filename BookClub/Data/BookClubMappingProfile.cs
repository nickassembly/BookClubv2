using AutoMapper;
using BookClub.Data.Entities;
using BookClub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Data
{
    public class BookClubMappingProfile : Profile
    {
        public BookClubMappingProfile()
        {
            CreateMap<Book, BookViewModel>()
                .ReverseMap();

            CreateMap<Author, AuthorViewModel>()
                .ReverseMap();

        }
    }
}
