using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Handlers.UrlLookup.Queries.GetAll;
using Application.Handlers.UrlLookup.Queries.GetByKey;
using Application.Handlers.UrlLookup.Queries.Shared;
using Application.Tests.Fixtures;
using Application.Tests.Helpers;
using AutoMapper;
using Bogus;
using Bogus.Extensions;
using FluentAssertions;
using Persistence;
using Xunit;

namespace Application.Tests.Handlers.UrlLookup.Queries.GetAll
{
    public class GetAllUrlLookupsQueryHandlerTests : IDisposable
    {
        private readonly ContextTestFixture  _fixture;
        private          UrlShortenerContext Context => _fixture.Context;
        private          IMapper             Mapper  => _fixture.Mapper;
        private          Faker               faker = new Faker();
        
        public GetAllUrlLookupsQueryHandlerTests()
        {
            _fixture = new ContextTestFixture();
        }

        [Fact]
        public async Task Handle_NoLookups_ReturnsEmptyListView()
        {
            // Arrange
            var getAllUrlLookupsQueryHandler = new GetAllUrlLookupsQueryHandler(Context, Mapper);

            // Act
            var result = await getAllUrlLookupsQueryHandler.Handle(new GetAllUrlLookupsQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Lookups.Should().NotBeNull().And.BeEmpty();
        }
        
        [Fact]
        public async Task Handle_ContainsUrlLookups_ReturnsListViewInOrderOfUrl()
        {
            // Arrange
            var urlLookupFaker = new Faker<Domain.Models.UrlLookup>()
                                .StrictMode(true)
                                .RuleFor(lookup => lookup.Key, _ => TestKeyGenerator.GetKey())
                                .RuleFor(lookup => lookup.Url, localFaker => localFaker.Internet.Url());

            var urlLookups = urlLookupFaker.GenerateBetween(5, 10);
            
            await Context.UrlLookups.AddRangeAsync(urlLookups);
            await Context.SaveChangesAsync();
            
            var getAllUrlLookupsQueryHandler = new GetAllUrlLookupsQueryHandler(Context, Mapper);

            // Act
            var result = await getAllUrlLookupsQueryHandler.Handle(new GetAllUrlLookupsQuery(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Lookups.Should().NotBeEmpty()
                  .And.BeInAscendingOrder(lookup => lookup.Url);
        }

        public void Dispose()
        {
            _fixture?.Dispose();
        }
    }
}