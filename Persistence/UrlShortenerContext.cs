using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence
{
    public class UrlShortenerContext : DbContext, IUrlShortenerContext
    {
        #region Constructors

        public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options) : base(options) { }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UrlShortenerContext).Assembly);
        }

        public DbSet<UrlLookup>      UrlLookups         { get; set; }
        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }
    }
}