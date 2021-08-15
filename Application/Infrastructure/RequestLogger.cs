using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Infrastructure
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        #region Constructors

        public RequestLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        #endregion

        private readonly ILogger<TRequest> _logger;

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            _logger.LogInformation($"Web request: {name} {request}");

            return Task.CompletedTask;
        }
    }
}