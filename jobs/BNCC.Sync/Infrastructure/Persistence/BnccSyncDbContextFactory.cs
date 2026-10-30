using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class BnccSyncDbContextFactory(
    DbContextOptions<BnccSyncDbContext> options)
    : IDbContextFactory<BnccSyncDbContext>
{
    public BnccSyncDbContext CreateDbContext() 
        => new BnccSyncDbContext(options);
    

    public Task<BnccSyncDbContext> CreateDbContextAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(CreateDbContext());
    }
}