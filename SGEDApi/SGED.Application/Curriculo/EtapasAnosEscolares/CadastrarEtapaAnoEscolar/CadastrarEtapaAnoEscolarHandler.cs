using SGED.Application.Common;
using SGED.Domain.Common.Results;
using SGED.Domain.Curriculo.EtapasAnosEscolares;

namespace SGED.Application.Curriculo.EtapasAnosEscolares.CadastrarEtapaAnoEscolar;

public class CadastrarEtapaAnoEscolarHandler(
    IEtapaAnoEscolarRepository repository,
    IUnitOfWork unitOfWork)
{
    public async Task<Result<Guid>> Handle(CadastrarEtapaAnoEscolarCommand command,
        CancellationToken cancellationToken)
    {
        var result = EtapaAnoEscolar.Create(
            command.NomeEtapaAnoEscolar,
            command.Codigo,
            command.Modalidade,
            command.Ordem);

        if (result.IsFailure)
            return Result<Guid>.Failure(result.Error);

        var etapa = result.Value;

        await repository.AddAsync(etapa!, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<Guid>.Success(etapa!.Id);
    }
}