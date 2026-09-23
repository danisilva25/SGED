using SGED.Application.Common;
using SGED.Domain.Common.Results;

namespace SGED.Application.Curriculo.EtapasAnosEscolares.AlterarEtapaAnoEscolar;

public class AlterarEtapaAnoEscolarHandler(
    IEtapaAnoEscolarRepository repository,
    IUnitOfWork unitOfWork)
{
    public async Task<Result<bool>> Handle(AlterarEtapaAnoEscolarCommand command, CancellationToken cancellationToken)
    {
        var etapa = await repository.GetByIdAsync(command.IdEtapaAnoEscolar, cancellationToken);

        if (etapa is null)
            return Result<bool>.Failure(new Error(
                "EtapaAnoEscolar.NaoEncontrada",
                "Etapa ano escolar não encontrada",
                ErrorType.NotFound
                ));
        
        etapa.Update(command.NomeEtapaAnoEscolar,
            command.Codigo,
            command.Modalidade,
            command.Ordem);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<bool>.Success(true);
    }
}