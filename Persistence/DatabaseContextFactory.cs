using Microsoft.EntityFrameworkCore;
using Persistence.Infrastructure;

namespace Persistence
{
    public class DatabaseContextFactory : DesignTimeDbContextFactoryBase<UrlShortenerContext>
    {
        protected override UrlShortenerContext CreateNewInstance(DbContextOptions<UrlShortenerContext> options)
        {
            UrlShortenerContext urlShortenerContext = new(options);
            urlShortenerContext.Database.EnsureCreated();
            
            return urlShortenerContext;
        }
    }
}