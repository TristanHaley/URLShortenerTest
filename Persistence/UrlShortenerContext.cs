using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class UrlShortenerContext : DbContext, IUrlShortenerContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UrlShortenerContext).Assembly);
        }
    }
}