using SGED.Application.Common;
using SGED.Domain.Common.Results;
using SGED.Domain.Curriculo.Disciplinas;

namespace SGED.Application.Curriculo.Disciplinas.CadastrarDisciplina;

public class CadastrarDisciplinaHandler(
    IDisciplinaRepository repository,
    IUnitOfWork unitOfWork)
{
    public async Task<Result<Guid>> Handle(CadastrarDisciplinaCommand command,
        CancellationToken cancellationToken)
    {
        var result = Disciplina.Create(command.Nome);

        if (result.IsFailure)
            return Result<Guid>.Failure(result.Error);

        var disciplina = result.Value;

        await repository.AddAsync(disciplina!, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<Guid>.Success(disciplina!.Id);
    }
}