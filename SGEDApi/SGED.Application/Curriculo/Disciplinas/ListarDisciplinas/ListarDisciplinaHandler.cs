using SGED.Domain.Common.Results;
using SGED.Domain.Curriculo.Disciplinas;

namespace SGED.Application.Curriculo.Disciplinas.ListarDisciplinas;

public sealed class ListarDisciplinaHandler(IDisciplinaRepository repository)
{
    public async Task<Result<IReadOnlyList<Disciplina>>> Handle(CancellationToken cancellationToken)
    {
        var disciplinas = await repository.GetAllAsync(cancellationToken);

        return Result<IReadOnlyList<Disciplina>>.Success(disciplinas);
    }
}