using Application.Interfaces.Mapping;
using AutoMapper;

namespace Application.Handlers.UrlLookup.Queries.Shared
{
    public class UrlLookupModel : IHaveCustomMapping
    {
        public string Key                                   { get; set; }
        public string Url                                   { get; set; }
        
        public void   CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Domain.Models.UrlLookup, UrlLookupModel>();
        }
    }
}