using CandidateHub.Application.Interfaces;
using CandidateHub.Domain.Entities;

namespace CandidateHub.Infrastructure.Persistence
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

    }
}
