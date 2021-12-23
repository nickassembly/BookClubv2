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
            CreateMap<UserBook, UserBookViewModel>()
                .ReverseMap();
            CreateMap<Book, BookViewModel>()
                .ReverseMap();
            CreateMap<Author, AuthorViewModel>()
                .ReverseMap();
            CreateMap<RegisterUser, RegisterViewModel>()
                .ReverseMap();
            CreateMap<LoginUser, LoginViewModel>()
                .ReverseMap();
            CreateMap<LoginUser, RegisterViewModel>()
                .ReverseMap();
            CreateMap<GoogleBookVolume, GoogleBookVolumeInfoViewModel>()
                .ReverseMap();
        }
    }
}
