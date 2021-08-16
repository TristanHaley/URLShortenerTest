using Bogus;
using Domain.Models;

namespace Application.Tests.Helpers
{
    public static class TestFakerManager
    {
        public static Faker<UrlLookup> UrlLookupFaker()
        {
            return new Faker<UrlLookup>()
                  .RuleFor(urlLookup => urlLookup.Key, faker => faker.Random.Hash(8))
                  .RuleFor(urlLookup => urlLookup.Url, faker => faker.Internet.Url());
        }
    }
}