using CandidateHub.Domain.Entities;
using CandidateHub.Infrastructure.Persistence;

namespace CandidateHub.Test.Setup
{
    internal static class SetupCandidate
    {
        public const int ID1 = 1;
        public const string FIRST_NAME = "Test1";
        public const string LAST_NAME = "Candidate";
        public const string EMAIL1 = "test@candidate.com";
        public const string PHONE_NUMBER = "9854545856";
        public const string CALL_TIME_INTERVAL = "9 AM - 5 PM";
        public const string LINKEDIN_URL = "https://linkedin.com/in/test1";
        public const string GITHUB_URL = "https://github.com/test1";
        public const string COMMENTS = "This is for test only.";

        public const int ID2 = 2;
        public const string EMAIL2 = "test2@candidate.com";

        public static void SeedCandidate(ChDbContext dbContext)
        {
            var dbCandidate1 = new Candidate
            {
                Id = ID1,
                FirstName = FIRST_NAME,
                LastName = LAST_NAME,
                Email = EMAIL1,
                PhoneNumber = PHONE_NUMBER,
                CallTimeInterval = CALL_TIME_INTERVAL,
                LinkedInUrl = LINKEDIN_URL,
                GitHubUrl = GITHUB_URL,
                Comments = COMMENTS,
                LastUpdatedAt = DateTimeOffset.Now,
                LastUpdatedBy = "TEST"
            };
            dbContext.Candidates.Add(dbCandidate1);

            var dbCandidate2 = new Candidate
            {
                Id = ID2,
                FirstName = FIRST_NAME,
                LastName = LAST_NAME,
                Email = EMAIL2,
                PhoneNumber = PHONE_NUMBER,
                CallTimeInterval = CALL_TIME_INTERVAL,
                LinkedInUrl = LINKEDIN_URL,
                GitHubUrl = GITHUB_URL,
                Comments = COMMENTS,
                LastUpdatedAt = DateTimeOffset.Now,
                LastUpdatedBy = "TEST"
            };
            dbContext.Candidates.Add(dbCandidate2);
        }
    }
}
