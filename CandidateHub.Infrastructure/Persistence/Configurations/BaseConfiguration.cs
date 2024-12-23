using CandidateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidateHub.Infrastructure.Persistence.Configurations
{
    public abstract class BaseConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            builder.HasKey(h => h.Id);

            builder.Property(p => p.LastUpdatedBy)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
