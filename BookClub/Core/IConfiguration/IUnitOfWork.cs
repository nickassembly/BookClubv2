using BookClub.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IAuthorRepository Authors { get; }

        Task CompleteAsync();
    }
}
