using CandidateHub.Application.Common.Models;
using CandidateHub.Domain.Entities;
using System.Linq.Expressions;

namespace CandidateHub.Application.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<PageResponse<TEntity>> ListPagedAsync(Expression<Func<TEntity, bool>> filter = null, int page = 1, int pageSize = 25, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, CancellationToken cancellationToken = default);
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<TKey> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    }
}
