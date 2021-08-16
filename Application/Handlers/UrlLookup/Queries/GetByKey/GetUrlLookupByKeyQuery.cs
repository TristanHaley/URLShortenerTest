using Application.Handlers.UrlLookup.Queries.Shared;
using MediatR;

namespace Application.Handlers.UrlLookup.Queries.GetByKey
{
    public class GetUrlLookupByKeyQuery : IRequest<UrlLookupModel>
    {
        public string Key { get; }
        public GetUrlLookupByKeyQuery(string key)
        {
            Key = key;
        }
    }
}