using AutoMapper;
using BookClub.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
