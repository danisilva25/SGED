using Microsoft.EntityFrameworkCore;
using SGED.Application.Common;
using SGED.Domain.Curriculo.EtapasAnosEscolares;

namespace SGED.Infrastructure.Persistence;

public class SgedDbContext(DbContextOptions options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<EtapaAnoEscolar> EtapasAnosEscolares
        => Set<EtapaAnoEscolar>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SgedDbContext).Assembly);
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await base.SaveChangesAsync(cancellationToken); 
    }
}