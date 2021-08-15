using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.UrlLookup.Commands.Create
{
    public class UrlLookupCreated : INotification
    {
        public UrlLookupCreated(string key, string url)
        {
            Key = key;
            Url = url;
        }
        public string Key { get; }
        public string Url { get; }
    }

    public class UrlLookupCreatedHandler : INotificationHandler<UrlLookupCreated>
    {
        private readonly ILogger<UrlLookupCreated> _logger;
        public UrlLookupCreatedHandler(ILogger<UrlLookupCreated> logger)
        {
            _logger = logger;
        }

        public Task Handle(UrlLookupCreated notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Created \"{@notification}\"", notification);
            return Task.CompletedTask;
        }
    }
}