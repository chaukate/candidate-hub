using CandidateHub.Application.Interfaces;
using CandidateHub.Domain.Entities;
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
}
