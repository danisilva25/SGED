using Microsoft.EntityFrameworkCore;
using SGED.Application.Curriculo.EtapasAnosEscolares;
using SGED.Domain.Curriculo.EtapasAnosEscolares;
using SGED.Infrastructure.Persistence;

namespace SGED.Infrastructure.Curriculo.EtapaAnosEscolares;

public sealed class EtapaAnoEscolarRepository(SgedDbContext context)
    : IEtapaAnoEscolarRepository
{
    public async Task AddAsync(EtapaAnoEscolar etapaAnoEscolar, CancellationToken cancellationToken)
        => await context.EtapasAnosEscolares.AddAsync(etapaAnoEscolar, cancellationToken);

    public async Task<IReadOnlyList<EtapaAnoEscolar>> GetAllAsync(CancellationToken cancellationToken)
        => await context.EtapasAnosEscolares
            .AsNoTracking()
            .Where(e => e.DeletedAt == null)
            .OrderBy(e => e.Nome)
            .ToListAsync(cancellationToken);

    public async Task<EtapaAnoEscolar?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        =>  await context.EtapasAnosEscolares
            .FirstOrDefaultAsync(e => e.Id == id && e.DeletedAt == null, cancellationToken);
}