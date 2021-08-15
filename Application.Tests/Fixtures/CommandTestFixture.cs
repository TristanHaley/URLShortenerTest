using System;
using Application.Tests.Infrastructure;
using AutoMapper;
using Persistence;
using Xunit;

namespace Application.Tests.Fixtures
{
    public class CommandTestFixture : IDisposable
    {
        #region Constructors

        public CommandTestFixture()
        {
            Context = TestDatabaseContextFactory.Create();
            Mapper  = AutoMapperFactory.Create();
        }

        #endregion

        public IMapper                Mapper         { get; }
        public UrlShortenerContext Context { get; }

        public void Dispose()
        {
            TestDatabaseContextFactory.Destroy(Context);
        }
    }
}