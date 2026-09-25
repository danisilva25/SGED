using Microsoft.EntityFrameworkCore;
using SGED.Application.Curriculo.Modalidades;
using SGED.Domain.Curriculo.EtapasAnosEscolares;
using SGED.Domain.Curriculo.Modalidades;
using SGED.Infrastructure.Persistence;

namespace SGED.Infrastructure.Curriculo.Modalidades;

public class ModalidadeRepository(SgedDbContext context) 
    : IModalidadeRepository
{
    public async Task AddAsync(Modalidade modalidade, CancellationToken cancellationToken)
        => await context.Modalidades.AddAsync(modalidade, cancellationToken);

    public async Task<IReadOnlyList<Modalidade>> GetAllAsync(CancellationToken cancellationToken)
        => await context.Modalidades
            .AsNoTracking()
            .Where(m => m.DeletedAt == null)
            .OrderBy(x => x.Codigo)
            .ToListAsync(cancellationToken);

    public Task<Modalidade?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        =>  context.Modalidades.FirstOrDefaultAsync(m => m.Id == id && m.DeletedAt == null, cancellationToken);
}