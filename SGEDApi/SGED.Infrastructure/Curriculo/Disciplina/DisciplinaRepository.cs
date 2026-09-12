using Microsoft.EntityFrameworkCore;
using SGED.Application.Curriculo.Disciplinas;
using SGED.Infrastructure.Persistence;

namespace SGED.Infrastructure.Curriculo.Disciplina;

public sealed class DisciplinaRepository(
    SgedDbContext context) : IDisciplinaRepository
{
    public async Task AddAsync(Domain.Curriculo.Disciplinas.Disciplina disciplina, 
        CancellationToken cancellationToken)
    {
        await context.Disciplinas.AddAsync(disciplina, cancellationToken);
    }

    public async Task<IReadOnlyList<Domain.Curriculo.Disciplinas.Disciplina>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Disciplinas
            .AsNoTracking()
            .OrderBy(d => d.Nome)
            .ToListAsync(cancellationToken);
    }

    public Task<Domain.Curriculo.Disciplinas.Disciplina> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}