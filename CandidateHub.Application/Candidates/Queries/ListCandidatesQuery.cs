using CandidateHub.Application.Common.Models;
using CandidateHub.Application.Interfaces;
using CandidateHub.Domain.Entities;
using MediatR;

namespace CandidateHub.Application.Candidates.Queries
{
    public class ListCandidatesHandler : IRequestHandler<ListCandidatesQuery, PageResponse<ListCandidatesResponse>>
    {
        private readonly IRepository<Candidate, int> _candidateRepo;
        public ListCandidatesHandler(IRepository<Candidate, int> candidateRepo)
        {
            _candidateRepo = candidateRepo;
        }

        public async Task<PageResponse<ListCandidatesResponse>> Handle(ListCandidatesQuery request, CancellationToken cancellationToken)
        {
            var pagedResult = await _candidateRepo.ListPagedAsync(page: request.Page, pageSize: request.PageSize, cancellationToken: cancellationToken);

            var response = new PageResponse<ListCandidatesResponse>
            {
                Page = pagedResult.Page,
                PageSize = pagedResult.PageSize,
                TotalRecords = pagedResult.TotalRecords,
                Result = pagedResult.Result.Select(s => new ListCandidatesResponse
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    CallTimeInterval = s.CallTimeInterval,
                    GitHubUrl = s.GitHubUrl,
                    LinkedInUrl = s.LinkedInUrl,
                    PhoneNumber = s.PhoneNumber,
                    LastUpdatedAt = s.LastUpdatedAt,
                    LastUpdatedBy = s.LastUpdatedBy
                }).ToList()
            };

            return response;
        }
    }

    public class ListCandidatesQuery : IRequest<PageResponse<ListCandidatesResponse>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }

    public class ListCandidatesResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CallTimeInterval { get; set; }
        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        public DateTimeOffset LastUpdatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
    }
}
