using CandidateHub.Application.Interfaces;
using CandidateHub.Domain.Entities;
using FluentValidation;
using MediatR;
using System.Text.Json.Serialization;

namespace CandidateHub.Application.Candidates.Commands
{
    public class UpsertCandidateHandler : IRequestHandler<UpsertCandidateCommand, int>
    {
        private readonly IRepository<Candidate, int> _candidateRepo;
        public UpsertCandidateHandler(IRepository<Candidate, int> candidateRepo)
        {
            _candidateRepo = candidateRepo;
        }

        public async Task<int> Handle(UpsertCandidateCommand request, CancellationToken cancellationToken)
        {
            var dbCandidate = await _candidateRepo.FindAsync(p => p.Email.ToLower() == request.Email.ToLower(), cancellationToken);
            if (dbCandidate == null)
            {
                dbCandidate ??= new Candidate();

                dbCandidate.FirstName = request.FirstName;
                dbCandidate.LastName = request.LastName;
                dbCandidate.Email = request.Email;
                dbCandidate.PhoneNumber = request.PhoneNumber;
                dbCandidate.CallTimeInterval = request.CallTimeInterval;
                dbCandidate.LinkedInUrl = request.LinkedInUrl;
                dbCandidate.GitHubUrl = request.GitHubUrl;
                dbCandidate.Comments = request.Comments;
                dbCandidate.LastUpdatedBy = request.CurrentUserName;
                dbCandidate.LastUpdatedAt = DateTimeOffset.Now;

                await _candidateRepo.AddAsync(dbCandidate, cancellationToken);
                return dbCandidate.Id;
            }
            else
            {
                dbCandidate.FirstName = request.FirstName;
                dbCandidate.LastName = request.LastName;
                dbCandidate.Email = request.Email;
                dbCandidate.PhoneNumber = request.PhoneNumber;
                dbCandidate.CallTimeInterval = request.CallTimeInterval;
                dbCandidate.LinkedInUrl = request.LinkedInUrl;
                dbCandidate.GitHubUrl = request.GitHubUrl;
                dbCandidate.Comments = request.Comments;
                dbCandidate.LastUpdatedBy = request.CurrentUserName;
                dbCandidate.LastUpdatedAt = DateTimeOffset.Now;

                await _candidateRepo.UpdateAsync(dbCandidate, cancellationToken);
                return dbCandidate.Id;
            }
        }
    }

    public class UpsertCandidateCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CallTimeInterval { get; set; }
        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        public string Comments { get; set; }

        [JsonIgnore]
        public string CurrentUserName { get; set; }

    }

    public class UpsertCondidateValidator : AbstractValidator<UpsertCandidateCommand>
    {
        public UpsertCondidateValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.")
                .MinimumLength(2)
                .WithMessage("Minimum character limit is 2.")
                .MaximumLength(255)
                .WithMessage("Maximum character limit is 255.");

            RuleFor(r => r.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MinimumLength(2)
                .WithMessage("Minimum character limit is 2.")
                .MaximumLength(255)
                .WithMessage("Maximum character limit is 255.");

            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email address.")
                .MaximumLength(255)
                .WithMessage("Maximum character limit is 255.");

            RuleFor(r => r.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Invalid phone number format.")
                .When(candidate => !string.IsNullOrEmpty(candidate.PhoneNumber));

            RuleFor(r => r.CallTimeInterval)
                .MaximumLength(50)
                .WithMessage("Maximum character limit is 50.")
                .Must(m =>
                {
                    var times = m.Split('-');
                    return DateTime.TryParse(times[0], out var start) &&
                           DateTime.TryParse(times[1], out var end) &&
                           start < end;
                })
                .WithMessage("Invalid time format. End time must be greater than start time.")
                .When(w => !string.IsNullOrEmpty(w.CallTimeInterval));

            RuleFor(r => r.LinkedInUrl)
                .MaximumLength(255)
                .WithMessage("Maximum character limit is 255.")
                .Must(m =>
                {
                    if (Uri.TryCreate(m, UriKind.Absolute, out var url))
                        return url.OriginalString.Contains("linkedin.com/in/");

                    return false;
                })
                .WithMessage("Invalid linkedin url.")
                .When(w => !string.IsNullOrEmpty(w.LinkedInUrl));

            RuleFor(r => r.GitHubUrl)
                .MaximumLength(255)
                .WithMessage("Maximum character limit is 255.")
                .Must(m =>
                {
                    if (Uri.TryCreate(m, UriKind.Absolute, out var url))
                        return url.OriginalString.Contains("github.com/");

                    return false;
                })
                .WithMessage("Invalid github url.")
                .When(w => !string.IsNullOrEmpty(w.GitHubUrl));

            RuleFor(r => r.Comments)
                .NotEmpty()
                .WithMessage("Comments are required.")
                .MinimumLength(25)
                .WithMessage("Minimum character limit is 25.");
        }
    }
}
