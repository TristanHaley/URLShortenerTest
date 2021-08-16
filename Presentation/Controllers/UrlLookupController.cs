using System.Threading.Tasks;
using Application.Handlers.UrlLookup.Commands.Create;
using Application.Handlers.UrlLookup.Queries.GetAll;
using Application.Handlers.UrlLookup.Queries.GetByKey;
using Application.Handlers.UrlLookup.Queries.GetByUrl;
using Application.Handlers.UrlLookup.Queries.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class UrlLookupController : ControllerBase
    {
        /// <summary> POST: Creates a URL Lookup </summary>
        /// <param name="urlLookupCommand"> The URL Lookup create command. </param>
        /// <returns> </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateUrlLookupCommand urlLookupCommand)
        {
            await Mediator.Send(urlLookupCommand).ConfigureAwait(false);
            return NoContent();
        }

        /// <summary> GET: Gets all the URL lookups. </summary>
        /// <returns> Ok </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UrlLookupListViewModel>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllUrlLookupsQuery())
                                   .ConfigureAwait(false));
        }

        /// <summary> GET: Gets a specific URL Lookup by Key </summary>
        /// <returns> Ok </returns>
        [HttpGet("{key}")]
        [ActionName("ByKey")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UrlLookupModel>> GetByKey(string key)
        {
            return Ok(await Mediator.Send(new GetUrlLookupByKeyQuery(key))
                                    .ConfigureAwait(false));
        }

        /// <summary> GET: Gets a specific URL Lookup by URL </summary>
        /// <returns> Ok </returns>
        [HttpGet("{url}")]
        [ActionName("ByUrl")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UrlLookupModel>> GetByUrl(string url)
        {
            return Ok(await Mediator.Send(new GetUrlLookupByUrlQuery(url))
                                    .ConfigureAwait(false));
        }
    }
}