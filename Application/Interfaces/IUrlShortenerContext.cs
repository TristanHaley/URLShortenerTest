using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUrlShortenerContext
    {
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(bool              acceptAllChangesOnSuccess, CancellationToken cancellationToken);
    }
}