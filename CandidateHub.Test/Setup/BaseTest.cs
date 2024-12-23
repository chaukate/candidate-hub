using CandidateHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CandidateHub.Test.Setup
{
    public abstract class BaseTest
    {
        protected static int _dbCount = 1;
        protected DbContextOptionsBuilder<ChDbContext> _dbContextBuilder;
        protected ChDbContext _dbContext;

        public BaseTest(string uniqueDbName)
        {
            _dbContextBuilder = new DbContextOptionsBuilder<ChDbContext>()
                                    .UseInMemoryDatabase(uniqueDbName);

            _dbContext = new ChDbContext(_dbContextBuilder.Options);

            SeedDb();
        }

        private void SeedDb()
        {
            using var dbContext = new ChDbContext(_dbContextBuilder.Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            SetupCandidate.SeedCandidate(dbContext);

            dbContext.SaveChanges();
        }
    }
}
