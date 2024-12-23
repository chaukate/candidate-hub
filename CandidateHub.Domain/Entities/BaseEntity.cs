namespace CandidateHub.Domain.Entities
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
