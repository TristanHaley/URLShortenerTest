using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Handlers.UrlLookup.Queries.GetByKey;
using Application.Handlers.UrlLookup.Queries.GetByUrl;
using Application.Handlers.UrlLookup.Queries.Shared;
using Application.Tests.Fixtures;
using Application.Tests.Helpers;
using AutoMapper;
using Bogus;
using FluentAssertions;
using Persistence;
using Xunit;

namespace Application.Tests.Handlers.UrlLookup.Queries.GetByUrl
{
    public class GetUrlLookupByUrlQueryHandlerTests : IDisposable
    {
        private readonly ContextTestFixture  _fixture;
        private          UrlShortenerContext Context => _fixture.Context;
        private          IMapper             Mapper  => _fixture.Mapper;
        private          Faker               faker = new Faker();
        
        public GetUrlLookupByUrlQueryHandlerTests()
        {
            _fixture = new ContextTestFixture();
        }

        [Fact]
        public async Task Handle_KeyDoesNotExist_ReturnsNullLookupModel()
        {
            // Arrange
            var url = faker.Internet.Url();
            
            var getUrlLookupByUrlQueryHandler = new GetUrlLookupByUrlQueryHandler(Context, Mapper);

            // Act
            var result = await getUrlLookupByUrlQueryHandler.Handle(new GetUrlLookupByUrlQuery(url), CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }
        
        [Fact]
        public async Task Handle_UrlDoesExist_ReturnsRelatedLookupModel()
        {
            // Arrange
            var key = TestKeyGenerator.GetKey();
            var url = faker.Internet.Url();

            var lookupModel = new Domain.Models.UrlLookup
            {
                Key = key,
                Url = url
            };
            await Context.UrlLookups.AddAsync(lookupModel);
            await Context.SaveChangesAsync();
            
            var getUrlLookupByUrlQueryHandler = new GetUrlLookupByUrlQueryHandler(Context, Mapper);

            // Act
            var result = await getUrlLookupByUrlQueryHandler.Handle(new GetUrlLookupByUrlQuery(url), CancellationToken.None);

            // Assert
            result.Should()
                  .NotBeNull()
                  .And.Match<UrlLookupModel>(lookup => lookup.Key == key &&
                                                       lookup.Url == url);
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
}