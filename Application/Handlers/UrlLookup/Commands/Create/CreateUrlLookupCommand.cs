using MediatR;

namespace Application.Handlers.UrlLookup.Commands.Create
{
    public class CreateUrlLookupCommand : IRequest<bool>
    {
        public string Url { get; set; }
    }
}