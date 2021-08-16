using System.Collections.Generic;
using Application.Handlers.UrlLookup.Queries.Shared;

namespace Application.Handlers.UrlLookup.Queries.GetAll
{
    public class UrlLookupListViewModel
    {
        public IList<UrlLookupModel> Lookups { get; set; }
    }
}