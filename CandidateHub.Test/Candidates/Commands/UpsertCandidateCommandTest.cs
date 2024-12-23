using CandidateHub.Application.Candidates.Commands;
using CandidateHub.Application.Common.Exceptions;
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

        [Fact]
        public async Task Update_ShouldUpdateEmployee()
        {
            // Arrange
            var command = new UpsertCandidateCommand
            {
                Id = SetupCandidate.ID1,
                FirstName = "updated-firstname",
                LastName = "updated-lastname",
                Email = "updated-email@candidate.com",
                PhoneNumber = "9854545855",
                CallTimeInterval = "8 AM - 4 PM",
                Comments = "This is a fact updated test to update a user.",
                CurrentUserName = "update-test",
                GitHubUrl = "http://github.com/testupdatedfn",
                LinkedInUrl = "http://linkedin.com/in/testupdatedfn"
            };

            var handler = new UpsertCandidateHandler(_repository);
            var cancellationToken = new CancellationToken();

            // Act
            var result = await handler.Handle(command, cancellationToken);

            // Assert
            Assert.Equal(command.Id, result);
        }

        [Theory]
        [InlineData(1000, "", "Candidate id is incorrect.")]
        [InlineData(SetupCandidate.ID1, SetupCandidate.EMAIL2, "Candidate already registered with email address.")]
        public async Task UpsertCandidateCommand_ExceptionTest(int id,
                                                               string email,
                                                               string exceptionMessage)
        {
            // Arrange
            var command = new UpsertCandidateCommand
            {
                Id = id,
                FirstName = "firstname",
                LastName = "lastname",
                Email = email,
                PhoneNumber = "9855555544",
                CallTimeInterval = "9 AM - 6 PM",
                Comments = "This is the test comment.",
                GitHubUrl = "https://github.com/testuser",
                LinkedInUrl = "https://linkedin.com/in/testuser",
                CurrentUserName = "TEST"
            };

            var handler = new UpsertCandidateHandler(_repository);
            var cancellationToken = new CancellationToken();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(async () => await handler.Handle(command, cancellationToken));
            Assert.Equal(exceptionMessage, exception.Message);
        }

        [Theory]
        [InlineData("", "", "", "", "", "", "", "",
                    new string[] { "First name is required.",
                                   "Last name is required.",
                                   "Email is required.",
                                   "Comments are required."})]
        [InlineData("f", "l", "invalid-email", "invalid-phone", "invalid-calltime", "invalid-linked", "invalid-github", "comment",
                    new string[] { "Minimum character limit is 2.",
                                   "Minimum character limit is 2.",
                                   "Invalid email address.",
                                   "Invalid phone number format.",
                                   "Invalid time format. End time must be greater than start time.",
                                   "Invalid linkedin url.",
                                   "Invalid github url.",
                                   "Minimum character limit is 25."})]
        [InlineData($"firstname-{LongString256}", $"lastname-{LongString256}", $"email_{LongString256}@test.com", "9888555510", "9 AM - 6 PM", "This is a test comments.", $"git-{LongString256}", $"git-{LongString256}",
                    new string[] { "Maximum character limit is 255.",
                                   "Maximum character limit is 255.",
                                   "Maximum character limit is 255.",
                                   "Maximum character limit is 255.",
                                   "Maximum character limit is 255."})]
        [InlineData("firstname", "lastname", "email@candidate.com", "9888555522", "9 AM - 6 PM", "This is a test comments.", "https://github.com/username", "https://linkedin.com/in/username",
                    new string[] { })]
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
