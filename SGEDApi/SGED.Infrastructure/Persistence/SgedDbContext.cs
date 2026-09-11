using Microsoft.EntityFrameworkCore;
using SGED.Application.Common;
using SGED.Domain.Curriculo.Disciplinas;

namespace SGED.Infrastructure.Persistence;

public class SgedDbContext(DbContextOptions options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<Disciplina> Disciplinas
        => Set<Disciplina>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SgedDbContext).Assembly);
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await base.SaveChangesAsync(cancellationToken); 
    }
}