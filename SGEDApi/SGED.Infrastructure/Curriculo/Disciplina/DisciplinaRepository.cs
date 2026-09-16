using Microsoft.EntityFrameworkCore;
using SGED.Application.Curriculo.Disciplinas;
using SGED.Infrastructure.Persistence;

namespace SGED.Infrastructure.Curriculo.Disciplina;

public sealed class DisciplinaRepository(
    SgedDbContext context) : IDisciplinaRepository
{
    public async Task AddAsync(Domain.Curriculo.Disciplinas.Disciplina disciplina, 
        CancellationToken cancellationToken) 
        => await context.Disciplinas.AddAsync(disciplina, cancellationToken);

    public async Task<IReadOnlyList<Domain.Curriculo.Disciplinas.Disciplina>> GetAllAsync(CancellationToken cancellationToken)
        => await context.Disciplinas
            .AsNoTracking()
            .Where(d => d.DeletedAt == null)
            .OrderBy(d => d.Nome)
            .ToListAsync(cancellationToken);

    public async Task<Domain.Curriculo.Disciplinas.Disciplina?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await context.Disciplinas.FirstOrDefaultAsync
            (d => d.Id == id, cancellationToken);
}