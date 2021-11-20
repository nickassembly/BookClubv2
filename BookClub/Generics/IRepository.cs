using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookClub.Generics
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class
    {
        IEnumerable<T> FindWithSpecificationPattern(ISpecification<T> specification = null);

    }
}
