using AutoMapper;
using BookClub.Data.Entities;
using BookClub.ViewModels;
using System.Collections.Generic;

namespace BookClub.Apis
{
    public class AuthorListProfile : Profile
    {
        public AuthorListProfile()
        {

            CreateMap<Author, AuthorListApiModel>();
            CreateMap<List<Author>, AuthorListResponse>()
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(o => o));

        }
    }
}
