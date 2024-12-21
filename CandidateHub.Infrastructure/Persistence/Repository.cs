using CandidateHub.Application.Interfaces;
using CandidateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CandidateHub.Infrastructure.Persistence
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly ChDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ChDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate,
                                             CancellationToken cancellationToken)
        {
            var result = await _dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken);
            return result;
        }

        public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken)
        {
            var result = await _dbSet.FindAsync(id, cancellationToken);
            return result;
        }

        public async Task<TKey> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
