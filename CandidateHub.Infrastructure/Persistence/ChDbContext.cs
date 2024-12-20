using CandidateHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CandidateHub.Infrastructure.Persistence
{
    public class ChDbContext : DbContext
    {
        public ChDbContext(DbContextOptions<ChDbContext> options) : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
