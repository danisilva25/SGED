using Microsoft.EntityFrameworkCore;
using SGED.Application.Common;
using SGED.Domain.Curriculo.EtapasAnosEscolares;
using SGED.Domain.Curriculo.Modalidades;

namespace SGED.Infrastructure.Persistence;

public class SgedDbContext(DbContextOptions options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<EtapaAnoEscolar> EtapasAnosEscolares
        => Set<EtapaAnoEscolar>();
    
    public DbSet<Modalidade>  Modalidades
        => Set<Modalidade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SgedDbContext).Assembly);
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await base.SaveChangesAsync(cancellationToken); 
    }
}