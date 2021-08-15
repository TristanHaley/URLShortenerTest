using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence.Infrastructure
{
    public abstract class DesignTimeDbContextFactoryBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
        where TDbContext : DbContext
    {
        private const string asp_net_core_environment = "ASPNETCORE_ENVIRONMENT";
        private const string connection_string_name   = "UrlShortenerConnection";

        public TDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}Presentation", Path.DirectorySeparatorChar);
            return Create(basePath, Environment.GetEnvironmentVariable(asp_net_core_environment));
        }

        private TDbContext Create(string basePath, string environmentName)
        {
            var configuration = new ConfigurationBuilder()
                               .SetBasePath(basePath)
                               .AddJsonFile("appsettings.json")
                               .AddJsonFile($"appsettings.{environmentName}.json", true)
                               .Build();

            var connectionString = configuration.GetConnectionString(connection_string_name);

            return Create(connectionString);
        }

        private TDbContext Create(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) throw new ArgumentException("Connection string is null or empty.", nameof(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            optionsBuilder.UseSqlite(connectionString);

            return CreateNewInstance(optionsBuilder.Options);
        }

        protected abstract TDbContext CreateNewInstance(DbContextOptions<TDbContext> options);
    }
}