using SGED.Domain.Curriculo.Disciplinas;

namespace SGED.Application.Curriculo.Disciplinas;

public interface IDisciplinaRepository
{
    Task AddAsync(Disciplina disciplina, CancellationToken cancellationToken);
}