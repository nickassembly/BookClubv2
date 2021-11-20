using BookClub.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookClub.Generics
{
    public interface IRepositoryWrapper
    {
            IBookRepository Book { get; }
            void Save();
        }

    }
}
