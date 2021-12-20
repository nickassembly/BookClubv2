using BookClub.Data.Entities;
using BookClub.Generics;
using BookClub.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookClub.Data.Entities
{
    public interface IBookRepository : IRepositoryBase<UserBook>
    {

    }
}