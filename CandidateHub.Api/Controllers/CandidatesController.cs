using CandidateHub.Application.Candidates.Commands;
using CandidateHub.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CandidateHub.Api.Controllers
{
    public class CandidatesController : BaseController<CandidatesController>
    {
        public CandidatesController(ILogger<CandidatesController> logger) : base(logger) { }

        [HttpGet]
        public async Task<IActionResult> List(CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Upsert(UpsertCandidateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                command.CurrentUserName = CurrentUserName;
                var response = await Mediator.Send(command, cancellationToken);
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                var errorMessage = $"{ex.Message} \n {ex.InnerException?.Message}";
                Logger.LogError($"Error: {errorMessage} \n StackTrace: {ex.StackTrace}");
                return StatusCode(500, errorMessage);
            }
        }
    }
}
