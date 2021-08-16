using Application.Handlers.UrlLookup.Queries.Shared;
using MediatR;

namespace Application.Handlers.UrlLookup.Queries.GetByUrl
{
    public class GetUrlLookupByUrlQuery : IRequest<UrlLookupModel>
    {
        public string Url { get; }
        public GetUrlLookupByUrlQuery(string url)
        {
            Url = url;
        }
    }
}