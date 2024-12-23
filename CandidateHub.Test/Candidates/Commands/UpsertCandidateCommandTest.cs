using CandidateHub.Application.Interfaces;
using CandidateHub.Domain.Entities;
using CandidateHub.Infrastructure.Persistence;
using CandidateHub.Test.Setup;

namespace CandidateHub.Test.Candidates.Commands
{
    internal class UpsertCandidateCommandTest : BaseTest
    {
        private readonly IRepository<Candidate, int> _repository;
        public UpsertCandidateCommandTest() : base(nameof(UpsertCandidateCommandTest) + _dbCount++)
        {
            _repository = new Repository<Candidate, int>(_dbContext);
        }
    }
}
