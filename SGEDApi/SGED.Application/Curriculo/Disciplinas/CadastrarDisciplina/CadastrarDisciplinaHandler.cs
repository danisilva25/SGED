using SGED.Application.Common;
using SGED.Domain.Curriculo.Disciplinas;

namespace SGED.Application.Curriculo.Disciplinas.CadastrarDisciplina;

public class CadastrarDisciplinaHandler(
    IDisciplinaRepository repository,
    IUnitOfWork unitOfWork)
{
    public async Task<Guid> Handle(CadastrarDisciplinaCommand command,
        CancellationToken cancellationToken)
    {
        var disciplina = Disciplina.Create(command.Nome);

        await repository.AddAsync(disciplina, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return disciplina.Id;
    }
}