using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CandidateHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TController> : ControllerBase
    {
        protected ILogger<TController> Logger;
        public BaseController(ILogger<TController> logger)
        {
            Logger = logger;
        }

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected string CurrentUserName = "SA";
    }
}