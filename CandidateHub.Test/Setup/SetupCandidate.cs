using CandidateHub.Domain.Entities;
using CandidateHub.Infrastructure.Persistence;

namespace CandidateHub.Test.Setup
{
    internal static class SetupCandidate
    {
        public const int ID = 1;
        public const string FIRST_NAME = "Test1";
        public const string LAST_NAME = "Candidate";
        public const string EMAIL = "test@candidate.com";
        public const string PHONE_NUMBER = "9854545856";
        public const string CALL_TIME_INTERVAL = "9 AM - 5 PM";
        public const string LINKEDIN_URL = "https://linkedin.com/in/test1";
        public const string GITHUB_URL = "https://github.com/test1";
        public const string COMMENTS = "This is for test only.";

        public static void SeedCandidate(ChDbContext dbContext)
        {
            var dbCandidate = new Candidate
            {
                Id = ID,
                FirstName = FIRST_NAME,
                LastName = LAST_NAME,
                Email = EMAIL,
                PhoneNumber = PHONE_NUMBER,
                CallTimeInterval = CALL_TIME_INTERVAL,
                LinkedInUrl = LINKEDIN_URL,
                GitHubUrl = GITHUB_URL,
                Comments = COMMENTS,
                LastUpdatedAt = DateTimeOffset.Now,
                LastUpdatedBy = "TEST"
            };

            dbContext.Candidates.Add(dbCandidate);
        }
    }
}
