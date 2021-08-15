using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class UrlShortenerContext : DbContext, IUrlShortenerContext
    {
        public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UrlShortenerContext).Assembly);
        }
    }
}