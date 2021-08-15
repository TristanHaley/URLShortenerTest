// Created by: Haley, Tristan (th185132) on: 13/06/2019 at: 13:57.
// Project: Mercury\Mercury.Application
// Copyright: © 2020 NCR. All Rights Reserved.
// Filename: RequestValidationBehaviour.cs

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Infrastructure
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        #region Constructors

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<RequestValidationBehavior<TRequest, TResponse>> logger)
        {
            _logger     = logger;
            _validators = validators;
        }

        #endregion

        private readonly ILogger<RequestValidationBehavior<TRequest, TResponse>> _logger;

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                          .Select(v => v.Validate(context))
                          .SelectMany(result => result.Errors)
                          .Where(f => f != null)
                          .ToList();

            if (failures.Count == 0) return next();

            var validationException = new ValidationException(failures);
            _logger.LogError(validationException, "Validation of request failed");

            throw validationException;
        }
    }
}