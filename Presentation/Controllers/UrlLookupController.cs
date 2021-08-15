using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class UrlLookupController : ControllerBase
    {
        public UrlLookupController(IMediator mediator) : base(mediator) { }
        
        
    }
}