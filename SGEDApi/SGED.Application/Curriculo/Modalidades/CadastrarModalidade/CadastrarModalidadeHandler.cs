using SGED.Application.Common;
using SGED.Domain.Common.Results;
using SGED.Domain.Curriculo.Modalidades;

namespace SGED.Application.Curriculo.Modalidades.CadastrarModalidade;

public class CadastrarModalidadeHandler(
    IModalidadeRepository repository,
    IUnitOfWork unitOfWork)
{
    public async Task<Result<Guid>> Handle(CadastrarModalidadeCommand command,
        CancellationToken cancellationToken)
    {
        var result = Modalidade.Create(command.Nome, command.Codigo);

        if (result.IsFailure)
            return Result<Guid>.Failure(result.Error);

        var modalidade = result.Value;

        await repository.AddAsync(modalidade!, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<Guid>.Success(modalidade!.Id);
    }
}