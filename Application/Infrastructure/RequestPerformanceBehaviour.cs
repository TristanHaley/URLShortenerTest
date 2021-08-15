// Created by: Haley, Tristan (th185132) on: 13/06/2019 at: 13:57.
// Project: Mercury\Mercury.Application
// Copyright: © 2020 NCR. All Rights Reserved.
// Filename: RequestPerformanceBehaviour.cs

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Infrastructure
{
    public sealed class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        #region Constructors

        public RequestPerformanceBehaviour(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();

            _logger = logger;
        }

        #endregion

        private readonly ILogger<TRequest> _logger;
        private readonly Stopwatch         _timer;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next().ConfigureAwait(false);

            _timer.Stop();

            if (_timer.ElapsedMilliseconds <= 500) return response;

            var name = typeof(TRequest).Name;
            _logger.LogWarning("Long-running web request: {Name} ({ElapsedMilliseconds} milliseconds) {@Request}", name, _timer.ElapsedMilliseconds, request);

            return response;
        }
    }
}