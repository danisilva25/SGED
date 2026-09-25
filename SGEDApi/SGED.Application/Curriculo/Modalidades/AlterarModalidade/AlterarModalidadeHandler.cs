using SGED.Application.Common;
using SGED.Domain.Common.Results;

namespace SGED.Application.Curriculo.Modalidades.AlterarModalidade;

public class AlterarModalidadeHandler(
    IModalidadeRepository repository,
    IUnitOfWork unitOfWork)
{
    public async Task<Result<bool>> Handle(AlterarModalidadeCommand command, CancellationToken cancellationToken)
    {
        var modalidade = await repository.GetByIdAsync(command.Id, cancellationToken);

        if (modalidade is null)
            return Result<bool>.Failure(new Error(
                "Modalidade.NaoEncontrado",
                "Modalidade não foi encontrada",
                ErrorType.NotFound
            ));

        modalidade.Update(
            command.Nome,
            command.Codigo);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<bool>.Success(true);
    }
}