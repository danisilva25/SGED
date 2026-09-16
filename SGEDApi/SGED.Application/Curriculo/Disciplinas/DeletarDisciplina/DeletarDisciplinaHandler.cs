using SGED.Application.Common;
using SGED.Domain.Common.Results;
using SGED.Domain.Curriculo.Disciplinas;

namespace SGED.Application.Curriculo.Disciplinas.DeletarDisciplina;

public class DeletarDisciplinaHandler(
    IDisciplinaRepository repository,
    IUnitOfWork unitOfWork)
{
    public async Task<Result<bool>> Handle(DeletarDisciplinaCommand command,
        CancellationToken cancellationToken)
    {
        var disciplina = await repository.GetByIdAsync(command.IdDisciplina, cancellationToken);

        if (disciplina is null)
            return Result<bool>.Failure(
                new Error(
                    "Disciplina.NaoEncontrada",
                    "Disciplina não encontrada",
                    ErrorType.NotFound));
        
        disciplina.MarkAsDeleted();
         
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<bool>.Success(true);
    }
}