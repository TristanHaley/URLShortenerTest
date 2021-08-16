using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Services;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.UrlLookup.Commands.Create
{
    public class CreateUrlLookupCommandHandler : IRequestHandler<CreateUrlLookupCommand, bool>
    {
        private readonly ILogger<CreateUrlLookupCommandHandler> _logger;
        private readonly IMediator                              _mediator;
        private readonly IUrlShortenerContext                   _context;
        private readonly IKeyGeneratorService                   _keyGeneratorService;

        public CreateUrlLookupCommandHandler(ILogger<CreateUrlLookupCommandHandler> logger, IMediator mediator, IUrlShortenerContext context, IKeyGeneratorService keyGeneratorService)
        {
            _logger                   = logger;
            _mediator                 = mediator;
            _context                  = context;
            _keyGeneratorService = keyGeneratorService;
        }
        
        public async Task<bool> Handle(CreateUrlLookupCommand request, CancellationToken cancellationToken)
        {
             _logger.LogDebug($"Handling \"{nameof(CreateUrlLookupCommand)}\" request, via \"{nameof(CreateUrlLookupCommandHandler)}\" handler");

             await using var contextTransaction = _context.BeginTransaction();

             try
             {
                 // Guard: URL already exists
                 if (await _context.UrlLookups.AnyAsync(urlLookup => urlLookup.Url == request.Url, cancellationToken)) return false;

                 var added = await _context.UrlLookups.AddAsync(new Domain.Models.UrlLookup
                 {
                     Key = await GetUniqueKeyAsync(),
                     Url = request.Url
                 }, cancellationToken);
                 
                 await _context.SaveChangesAsync(true, cancellationToken);
                 await contextTransaction.CommitAsync(cancellationToken);
                 await _mediator.Publish(new UrlLookupCreated(added.Entity.Key, added.Entity.Url), cancellationToken).ConfigureAwait(false);

                 return true;
             }
             catch (Exception ex)
             {
                 _logger.LogError(ex, "Unable to handle create request");
                 _logger.LogDebug("Rolling back transaction");
                 await contextTransaction.RollbackAsync(cancellationToken);
                 _logger.LogDebug("Rollback complete");
                 return false;
             }
        }

        private async Task<string> GetUniqueKeyAsync()
        {
            // TODO: A more sophisticated retry system than random luck
            // Consider use of Polly for retry mechanism and using url as seed
            
            
            const int maxRetries   = 10000;
            var       currentRetry = 0;

            do
            {
                var key = _keyGeneratorService.GenerateUniqueKey();
                if (await _context.UrlLookups.FindAsync(key) == null) return key;
            } while (currentRetry++ < maxRetries);

            _logger.LogWarning("Failed to generate a key within {MaxRetries} attempts", maxRetries);
            throw new ArgumentOutOfRangeException(nameof(maxRetries), "Maximum number of retries reached while generating a unique key");
        }
    }
}