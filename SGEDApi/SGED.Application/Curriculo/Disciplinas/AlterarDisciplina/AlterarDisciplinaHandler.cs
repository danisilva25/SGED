using SGED.Application.Common;
using SGED.Domain.Common.Results;

namespace SGED.Application.Curriculo.Disciplinas.AlterarDisciplina;

public class AlterarDisciplinaHandler(
    IDisciplinaRepository repository,
    IUnitOfWork unitOfWork)
{
    public async Task<Result<bool>> Handle(AlterarDisciplinaCommand command, CancellationToken cancellationToken)
    {
        var disciplina = await repository.GetByIdAsync(command.IdDisciplina, cancellationToken);

        if (disciplina is null)
            return Result<bool>.Failure(
                new Error(
                    "Disciplina.NaoEncontrada",
                    "Disciplina não encontrada",
                    ErrorType.NotFound));

        disciplina.UpdateName(command.NomeDisciplina);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<bool>.Success(true);
    }
}