using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Handlers.UrlLookup.Commands.Create;
using Application.Services;
using Application.Tests.Fixtures;
using Application.Tests.Helpers;
using AutoMapper;
using Bogus;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Persistence;
using Xunit;

namespace Application.Tests.Handlers.UrlLookup.Commands
{
    public class CreateUrlLookupCommandHandlerTests : IDisposable
    {
        private readonly CommandTestFixture  _fixture;
        private          UrlShortenerContext Context => _fixture.Context;
        private          IMapper             Mapper  => _fixture.Mapper;
        private          Faker               faker = new Faker();
        
        public CreateUrlLookupCommandHandlerTests()
        {
            _fixture = new CommandTestFixture();
        }

        [Fact]
        public async Task Handle_UrlDoesNotExist_CreatesNewEntryWithUrlAndUniqueKey()
        {
            // Arrange
            var logger              = Substitute.For<ILogger<CreateUrlLookupCommandHandler>>();
            var mediator            = Substitute.For<IMediator>();
            var keyGeneratorService = Substitute.For<IKeyGeneratorService>();

            var key     = new string(Guid.NewGuid().ToString().Take(8).ToArray());
            var url     = faker.Internet.Url();

            keyGeneratorService.GenerateUniqueKey().Returns(key);
            
            var handler = new CreateUrlLookupCommandHandler(logger, mediator, Context, keyGeneratorService);

            // Act
            var result = await handler.Handle(new CreateUrlLookupCommand
            {
                Url = url
            }, CancellationToken.None);

            // Assert
            result.Should().BeTrue();
            Context.UrlLookups.Should()
                   .HaveCount(1)
                   .And.ContainSingle(lookup => lookup.Key == key &&
                                                lookup.Url == url);
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
}