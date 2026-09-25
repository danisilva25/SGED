using SGED.Application.Common;
using SGED.Domain.Common.Results;

namespace SGED.Application.Curriculo.Modalidades.DeletarModalidade;

public class DeletarModalidadeHandler(
    IModalidadeRepository repository,
    IUnitOfWork unitOfWork)
{
    public async Task<Result<bool>> Handle(DeletarModalidadeCommand commmand, CancellationToken  cancellationToken)
    {
        var modalidade = await repository.GetByIdAsync(commmand.Id, cancellationToken);

        if (modalidade is null)
            return Result<bool>.Failure(new Error(
                "Modalidade.NaoEncontrada",
                "A modalidade não foi encontrada",
                ErrorType.NotFound
                ));
        
        modalidade.MarkAsDeleted();

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<bool>.Success(true);
    }
}