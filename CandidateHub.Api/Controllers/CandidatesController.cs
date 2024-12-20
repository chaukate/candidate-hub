using Microsoft.AspNetCore.Mvc;

namespace CandidateHub.Api.Controllers
{
    public class CandidatesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> List(CancellationToken cancellationToken)
        {
            return Ok();
        }
    }
}
