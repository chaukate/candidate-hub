using CandidateHub.Application.Common.Exceptions;
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
            var dbCandidate = await _candidateRepo.GetByIdAsync(request.Id, cancellationToken);

            if (request.Id > 0 && dbCandidate == null)
                throw new BadRequestException("Candidate id is incorrect.");

            if (await _candidateRepo.AnyAsync(a => (request.Id == 0 || dbCandidate.Email.ToLower() != request.Email.ToLower())
                                                   && a.Email.ToLower() == request.Email.ToLower(), cancellationToken))
                throw new BadRequestException("Candidate already registered with email address.");

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
        public int Id { get; set; }
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
                .MinimumLength(2)
                .WithMessage("Minimum character limit is 2.")
                .MaximumLength(255)
                .WithMessage("Maximum character limit is 255.")
                .When(w => !string.IsNullOrEmpty(w.FirstName))
                .NotEmpty()
                .WithMessage("First name is required.");

            RuleFor(r => r.LastName)
                .MinimumLength(2)
                .WithMessage("Minimum character limit is 2.")
                .MaximumLength(255)
                .WithMessage("Maximum character limit is 255.")
                .When(w => !string.IsNullOrEmpty(w.LastName))
                .NotEmpty()
                .WithMessage("Last name is required.");

            RuleFor(r => r.Email)
                .EmailAddress()
                .WithMessage("Invalid email address.")
                .MaximumLength(255)
                .WithMessage("Maximum character limit is 255.")
                .When(w => !string.IsNullOrEmpty(w.Email))
                .NotEmpty()
                .WithMessage("Email is required.");

            RuleFor(r => r.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Invalid phone number format.")
                .When(candidate => !string.IsNullOrEmpty(candidate.PhoneNumber));

            RuleFor(r => r.CallTimeInterval)
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
                .MinimumLength(25)
                .WithMessage("Minimum character limit is 25.")
                .When(w => !string.IsNullOrEmpty(w.Comments))
                .NotEmpty()
                .WithMessage("Comments are required.");
        }
    }
}
