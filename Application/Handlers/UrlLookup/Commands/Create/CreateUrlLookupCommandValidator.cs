using System;
using FluentValidation;

namespace Application.Handlers.UrlLookup.Commands.Create
{
    public class CreateUrlLookupCommandValidator : AbstractValidator<CreateUrlLookupCommand> 
    {
        public CreateUrlLookupCommandValidator()
        {
            RuleFor(x => x.Url)
               .NotNull()
               .NotEmpty()
               .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
                            (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps));
        }
    }
}