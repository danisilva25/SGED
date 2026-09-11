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
}