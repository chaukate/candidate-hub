using CandidateHub.Domain.Entities;

namespace CandidateHub.Application.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

    }
}
