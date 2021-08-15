using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Interfaces
{
    public interface IUrlShortenerContext
    {
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        
        public DbSet<UrlLookup> UrlLookups { get; set; }

        public abstract IDbContextTransaction BeginTransaction();
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(bool              acceptAllChangesOnSuccess, CancellationToken cancellationToken);
    }
}