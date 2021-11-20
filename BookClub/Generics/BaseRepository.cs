using BookClub.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookClub.Generics
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected BookClubContext _repositoryContext { get; set; }
        public Repository(BookClubContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Ardalis.Specification.ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> FindWithSpecificationPattern(ISpecification<T> specification = null)
        {
            throw new NotImplementedException();
        }


        public Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetBySpecAsync<Spec>(Spec specification, CancellationToken cancellationToken = default) where Spec : Ardalis.Specification.ISingleResultSpecification, Ardalis.Specification.ISpecification<T>
        {
            throw new NotImplementedException();
        }

        public Task<TResult> GetBySpecAsync<TResult>(Ardalis.Specification.ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ListAsync(Ardalis.Specification.ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> ListAsync<TResult>(Ardalis.Specification.ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        Task<T?> Ardalis.Specification.IReadRepositoryBase<T>.GetByIdAsync<TId>(TId id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<T?> Ardalis.Specification.IReadRepositoryBase<T>.GetBySpecAsync<Spec>(Spec specification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
