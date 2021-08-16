using System.Data;
using FluentValidation;

namespace Application.Handlers.UrlLookup.Queries.GetByKey
{
    public class GetUrlLookupByKeyQueryValidator : AbstractValidator<GetUrlLookupByKeyQuery>
    {
        public GetUrlLookupByKeyQueryValidator()
        {
            RuleFor(x => x.Key).NotNull().NotEmpty();
        }
    }
}