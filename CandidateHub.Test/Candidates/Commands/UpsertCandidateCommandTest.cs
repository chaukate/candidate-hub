using CandidateHub.Application.Candidates.Commands;
using CandidateHub.Application.Interfaces;
using CandidateHub.Domain.Entities;
using CandidateHub.Infrastructure.Persistence;
using CandidateHub.Test.Setup;
using Xunit;

namespace CandidateHub.Test.Candidates.Commands
{
    public class UpsertCandidateCommandTest : BaseTest
    {
        private readonly IRepository<Candidate, int> _repository;
        public UpsertCandidateCommandTest() : base(nameof(UpsertCandidateCommandTest) + _dbCount++)
        {
            _repository = new Repository<Candidate, int>(_dbContext);
        }

        [Fact]
        public async Task Create_ShouldCreateEmployee()
        {
            // Arrange
            var command = new UpsertCandidateCommand
            {
                FirstName = "testcrfn",
                LastName = "testcrln",
                Email = "testcr@candidate.com",
                PhoneNumber = "9855551100",
                CallTimeInterval = "9 AM - 5 PM",
                Comments = "This is a fact test to create a user.",
                CurrentUserName = "fact-test",
                GitHubUrl = "http://github.com/testcrfn",
                LinkedInUrl = "http://linkedin.com/in/testcrfn",
            };

            var handler = new UpsertCandidateHandler(_repository);
            var cancellationToken = new CancellationToken();

            // Act
            var result = await handler.Handle(command, cancellationToken);

            // Assert
            Assert.True(result > 0);
        }

        [Theory]
        [InlineData("", "", "", "", "", "", "", "",
                    new string[] { "First name is required.",
                                   "Last name is required.",
                                   "Email is required.",
                                   "Comments are required."})]
        public async Task UpsertCandidateValidator_Test(string firstName,
                                                        string lastName,
                                                        string email,
                                                        string phoneNumber,
                                                        string callTimeInterval,
                                                        string comments,
                                                        string gitHubUrl,
                                                        string linkedInUrl,
                                                        string[] errorMessages)
        {
            // Arrange
            var command = new UpsertCandidateCommand
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                CallTimeInterval = callTimeInterval,
                Comments = comments,
                GitHubUrl = gitHubUrl,
                LinkedInUrl = linkedInUrl,
                CurrentUserName = "validator-test"
            };
            var validator = new UpsertCondidateValidator();
            var cancellationToken = new CancellationToken();

            // Act
            var result = await validator.ValidateAsync(command, cancellationToken);

            // Assert
            Assert.NotNull(result);
            if (errorMessages.Length > 0)
            {
                Assert.False(result.IsValid);
                Assert.Equal(errorMessages.Length, result.Errors.Count);

                foreach (var errorMessage in errorMessages)
                {
                    Assert.Contains(result.Errors, a => a.ErrorMessage == errorMessage);
                }
            }
            else
            {
                Assert.True(result.IsValid);
            }
        }
    }
}
