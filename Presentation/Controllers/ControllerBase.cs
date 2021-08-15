using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class ControllerBase
    {
        #region Constructors

        protected ControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        #endregion

        private IMediator _mediator;
    }
}