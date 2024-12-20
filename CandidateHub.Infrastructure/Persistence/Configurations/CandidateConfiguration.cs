using CandidateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CandidateHub.Infrastructure.Persistence.Configurations
{
    internal class CandidateConfiguration : BaseConfiguration<Candidate, int>
    {
        public override void Configure(EntityTypeBuilder<Candidate> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.FirstName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasMaxLength(255)
                .IsRequired();
            builder.HasIndex(h => h.Email)
                .IsUnique();

            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(p => p.CallTimeInterval)
                .HasMaxLength(20)
                .IsRequired(false);

            builder.Property(p => p.LinkedInUrl)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(p => p.GitHubUrl)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(p => p.Comments)
                .IsRequired();
        }
    }
}
