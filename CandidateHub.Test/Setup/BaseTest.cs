using CandidateHub.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CandidateHub.Test.Setup
{
    public abstract class BaseTest
    {
        protected static int _dbCount = 1;
        protected DbContextOptionsBuilder<ChDbContext> _dbContextBuilder;
        protected ChDbContext _dbContext;

        protected const string LongString256 = "xJbs2XJmpgyijHq6vxGpdc9jmHQpqkTjF1qGUFhfD4ZUr3AqHV1lGvy0e1e1reotvq3geLVbGASsprspszamRCV9SqZ6V0zJ15HGW5HPDHqUqzO2F3r7Q9HtO85TvpM5kpPleoDUMOX1q2xKxko0fFcCgMbFU6n6IVoo9KVA5wpDkCKNUPM1SsWU7rb1MyRnYtZGSVOVWiPosyW0C9oddlO95NUFp40A3fhTQYOmkpIkfPBG07Mj5RtYkjKTYqgV";

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
