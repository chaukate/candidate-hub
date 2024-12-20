using CandidateHub.Application.Interfaces;
using CandidateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken)
        {
            var result = await _dbSet.FindAsync(id, cancellationToken);
            return result;
        }
    }
}
