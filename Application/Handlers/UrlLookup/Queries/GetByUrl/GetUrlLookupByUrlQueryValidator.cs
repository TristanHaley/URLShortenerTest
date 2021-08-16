using System;
using FluentValidation;

namespace Application.Handlers.UrlLookup.Queries.GetByUrl
{
    public class GetUrlLookupByUrlQueryValidator : AbstractValidator<GetUrlLookupByUrlQuery> 
    {
        public GetUrlLookupByUrlQueryValidator()
        {
            RuleFor(x => x.Url)
               .NotNull()
               .NotEmpty()
               .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
                            (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps));
        }
    }
}